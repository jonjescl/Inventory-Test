using Microsoft.AspNetCore.Mvc;
using ProductServices.Models;
using ProductServices.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _ProductsRepository;

        public ProductsController(IProductsRepository ProductsRepository)
        {
            _ProductsRepository = ProductsRepository;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<List<MProducts>> Get()
        {
            return await _ProductsRepository.ProductsList();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<MResponse> Post([FromBody] MProducts value)
        {
            MResponse obj = new MResponse();
            obj.IsOk = false;
            try
            {
                if (string.IsNullOrWhiteSpace(value.Name))
                {
                    obj.Message = "Error: debe ingresar nombre del producto.";
                }
                else if (string.IsNullOrWhiteSpace(value.Description))
                {
                    obj.Message = "Error: debe ingresar descripción del producto.";
                }
                else if (value.Price <= 0)
                {
                    obj.Message = "Error: debe ingresar precio del producto.";
                }
                else if (value.Stock <= 0)
                {
                    obj.Message = "Error: debe ingresar stock del producto.";
                }
                else if (value.CategoryId <= 0)
                {
                    obj.Message = "Error: debe ingresar categoría del producto.";
                }
                else
                {
                    obj = await _ProductsRepository.ProductsCreate(value);
                }
                return obj;
            }
            catch (Exception)
            {
                obj.Message = "Error: está intentando de ingresar parámetros incorrectos.";
                return obj;
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{Id}")]
        public async Task<MResponse> Put(int Id, [FromBody] MProducts value)
        {
            MResponse obj = new MResponse();
            obj.IsOk = false;
            try
            {
                if (Id <=0)
                {
                    obj.Message = "Error: debe ingresar Id del producto.";
                }
                else if (string.IsNullOrWhiteSpace(value.Name))
                {
                    obj.Message = "Error: debe ingresar nombre del producto.";
                }
                else if (string.IsNullOrWhiteSpace(value.Description))
                {
                    obj.Message = "Error: debe ingresar descripción del producto.";
                }
                else if (value.Price <= 0)
                {
                    obj.Message = "Error: debe ingresar precio del producto.";
                }
                else if (value.Stock <= 0)
                {
                    obj.Message = "Error: debe ingresar stock del producto.";
                }
                else if (value.CategoryId <= 0)
                {
                    obj.Message = "Error: debe ingresar categoría del producto.";
                }
                else
                {
                    value.Id = Id;
                    obj = await _ProductsRepository.ProductsModify(value);
                }
                return obj;
            }
            catch (Exception)
            {
                obj.Message = "Error: está intentando de ingresar parámetros incorrectos.";
                return obj;
            }
           
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{Id}")]
        public async Task<MResponse> Delete(int Id)
        {
            MResponse obj = new MResponse();
            obj.IsOk = false;
            try
            {
                if (Id <= 0)
                {
                    obj.Message = "Error: debe ingresar Id del producto.";
                }
                else
                {
                    obj = await _ProductsRepository.ProductsDelete(Id);
                }
                return obj;
            }
            catch (Exception)
            {
                obj.Message = "Error: está intentando de ingresar parámetros incorrectos.";
                return obj;
            }

           
        }

        [HttpPut("{Id}/AdjustStock")]
        public async Task<MResponse> AdjustStock(int Id, [FromBody] StockAdjustment request)
        {
            return await _ProductsRepository.ProductsStock(Id, request.Quantity);
            
        }
    }
}
