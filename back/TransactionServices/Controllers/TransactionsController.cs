using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Transactions;
using TransactionServices.Models;
using TransactionServices.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsRepository _TransactionsRepository;
        private readonly ITransactionProductsRepository _TransactionProductsRepository;
        private readonly HttpClient _HttpClient;
        public TransactionsController(ITransactionsRepository TransactionsRepositor, ITransactionProductsRepository TransactionProductsRepository, HttpClient HttpClient)
        {
            _TransactionsRepository = TransactionsRepositor;
            _TransactionProductsRepository = TransactionProductsRepository;
            _HttpClient = HttpClient;
        }
        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<List<MTransactions>> Get()
        {
            return await _TransactionsRepository.TransactionsList();
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{Id}")]
        public async Task<MTransactions> Get(int Id)
        {
            return await _TransactionsRepository.TransactionsDetails(Id);
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<MResponse> Post([FromBody] MTransactions value)
        {
            MResponse obj = new MResponse();
            obj.IsOk = false;
            try
            {
                if (value.TransactionTypeId <= 0)
                {
                    obj.Message = "Error: debe ingresar Id del tipo de tranacción (1.Compra 2.Venta).";
                }
                else if (value.TotalPrice <= 0)
                {
                    obj.Message = "Error: debe ingresar productos.";
                }
                else if (string.IsNullOrWhiteSpace(value.Detail))
                {
                    obj.Message = "Error: debe ingresar detalle.";
                }
                else if (value.TransactionProducts.Count < 1)
                {
                    obj.Message = "Error: debe ingresar al menos un producto.";
                }
                else
                {
                    var resT = await _TransactionsRepository.TransactionsCreate(value);
                    if (resT.IsOk == false)
                    {
                        obj.Message = "Error: Ha ocurrido un problema al registrar transacción.";
                        return obj;
                    }
                    else 
                    {
                        bool Ok=true;
                        string ProductName = "";
                        List<MTransactionProducts> listT = new List<MTransactionProducts>();

                        foreach (var item in value.TransactionProducts)
                        {
                            ProductName=item.ProductName;
                            int factor = (value.TransactionTypeId == 1) ? 1 : -1;
                            MStockAdjustment adj= new MStockAdjustment
                            {
                                Quantity = item.Quantity * factor
                            };

                            var content = new StringContent(
                                JsonSerializer.Serialize(adj),
                                Encoding.UTF8,
                                "application/json"
                            );

                            var res1 = await _HttpClient.PutAsync($"https://localhost:7051/api/Products/{item.ProductId}/AdjustStock", content);
                            var responseString = await res1.Content.ReadAsStringAsync();

                            // Deserializar el JSON a un objeto MResponse
                            var result = JsonSerializer.Deserialize<MResponse>(responseString, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                            if (result.IsOk == false)
                            {
                                Ok = false;
                                break;
                            }
                            else
                            {
                                listT.Add(item);
                            }
                        }

                        if (Ok==true)
                        {
                            foreach (var item in value.TransactionProducts)
                            {
                                await _TransactionProductsRepository.TransactionProductsCreate(int.Parse(resT.Message), item);
                                
                            }
                            obj.IsOk = true;
                            obj.Message = "Ok: Transacción registrada con éxito.";
                        }
                        else
                        {
                            //volvemos a aumentar stock ya que dió error en alguno
                            foreach (var item in listT)
                            {
                                MStockAdjustment adj = new MStockAdjustment
                                {
                                    Quantity = item.Quantity
                                };

                                var content = new StringContent(
                                    JsonSerializer.Serialize(adj),
                                    Encoding.UTF8,
                                    "application/json"
                                );

                                var res1 = await _HttpClient.PutAsync($"https://localhost:7051/api/Products/{item.ProductId}/AdjustStock", content);
                                var responseString = await res1.Content.ReadAsStringAsync();

                            }
                            obj.IsOk = false;
                            obj.Message = "Error: producto - "+ProductName+" - con stock insuficiente, la trnasacción será cancelada." ;
                        }
                        
                    }
                }
                return obj;
            }
            catch (Exception)
            {

                obj.Message = "Error: está intentando de ingresar parámetros incorrectos.";
                return obj;
            }
            
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{Id}")]
        public async Task<MResponse> Put(int Id, [FromBody] MTransactions value)
        {
            MResponse obj = new MResponse();
            obj.IsOk = false;
            try
            {
                if (Id <= 0)
                {
                    obj.Message = "Error: debe ingresar Id de la transacción.";
                }
                if (value.TransactionTypeId <= 0)
                {
                    obj.Message = "Error: debe ingresar Id del tipo de tranacción (1.Compra 2.Venta).";
                }
                else if (value.TotalPrice <= 0)
                {
                    obj.Message = "Error: debe ingresar productos.";
                }
                else if (string.IsNullOrWhiteSpace(value.Detail))
                {
                    obj.Message = "Error: debe ingresar detalle.";
                }
                else if (value.TransactionProducts.Count < 1)
                {
                    obj.Message = "Error: debe ingresar al menos un producto.";
                }
                else
                {

                    value.Id = Id;
                    obj = await _TransactionsRepository.TransactionsModify(value);

                }
                return obj;
            }
            catch (Exception)
            {
                obj.Message = "Error: está intentando de ingresar parámetros incorrectos.";
                return obj;
            }
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{Id}")]
        public async Task<MResponse> Delete(int Id)
        {
            MResponse obj = new MResponse();
            obj.IsOk = false;
            if (Id <= 0)
            {
                obj.Message = "Error: debe ingresar Id de la transacción.";
            }
            else 
            {
                obj = await _TransactionsRepository.TransactionsDelete(Id);
            }
            return obj;
        }
    }
}
