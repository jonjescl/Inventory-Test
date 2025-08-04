using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using TransactionServices.DB;
using TransactionServices.Models;

namespace TransactionServices.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly string ConnectionString = "";
        public TransactionsRepository(Connection _configuration)
        {
            ConnectionString = _configuration.ConnectionString;
        }
        public async Task<List<MTransactions>> TransactionsList()
        {
            List<MTransactions> TList = new List<MTransactions>();
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Transactions.sp_TransactionsList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    TList.Add(new MTransactions
                    {
                        Id = int.Parse(reader["Id"].ToString()!),
                        Detail = reader["Detail"].ToString()!,
                        Date = DateTime.Parse(reader["Date"].ToString()!),
                        TotalPrice = decimal.Parse(reader["TotalPrice"].ToString()!),
                        TransactionTypeId = int.Parse(reader["TransactionTypeId"].ToString()!),
                        TransactionType =new MTransactionTypes
                        {
                            Id = int.Parse(reader["TransactionTypeId"].ToString()!),
                            Name = reader["Name"].ToString()!,
                            CreatedAt = DateTime.Parse(reader["CreatedAtType"].ToString()!),
                            UpdatedAt = DateTime.Parse(reader["UpdatedAtType"].ToString()!),
                        },
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()!),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()!),
                    });
                }
            }
            catch (Exception)
            {
            }
            return TList;
        }
        public async Task<MTransactions> TransactionsDetails(int Id)
        {
            MTransactions? obj = new MTransactions();
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Transactions.sp_TransactionDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", Id);
                await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    obj = new MTransactions
                    {
                        Id = int.Parse(reader["Id"].ToString()!),
                        Detail = reader["Detail"].ToString()!,
                        Date = DateTime.Parse(reader["Date"].ToString()!),
                        TotalPrice = decimal.Parse(reader["TotalPrice"].ToString()!),
                        TransactionTypeId = int.Parse(reader["TransactionTypeId"].ToString()!),
                        TransactionType = new MTransactionTypes
                        {
                            Id = int.Parse(reader["TransactionTypeId"].ToString()!),
                            Name = reader["Name"].ToString()!,
                            CreatedAt = DateTime.Parse(reader["CreatedAtType"].ToString()!),
                            UpdatedAt = DateTime.Parse(reader["UpdatedAtType"].ToString()!),
                        },
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()!),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()!),
                    };
                }
                reader.Close();
                ///listado 
                List<MTransactionProducts> list = new List<MTransactionProducts>();
                using var command2 = new SqlCommand("Transactions.sp_TransactionProductsList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command2.Parameters.AddWithValue("@TransactionId", Id);


                using var reader2 = await command2.ExecuteReaderAsync();

                while (await reader2.ReadAsync())
                {

                    list.Add( new MTransactionProducts
                    {
                        Id = int.Parse(reader2["Id"].ToString()!),
                        ProductName = reader2["ProductName"].ToString()!,
                        Quantity = int.Parse(reader2["Quantity"].ToString()!),
                        TotalPrice = decimal.Parse(reader2["TotalPrice"].ToString()!),
                        ProductId = int.Parse(reader2["ProductId"].ToString()!),
                        CreatedAt = DateTime.Parse(reader2["CreatedAt"].ToString()!),
                        UpdatedAt = DateTime.Parse(reader2["UpdatedAt"].ToString()!),
                        TransactionId= int.Parse(reader2["TransactionId"].ToString()!),
                        UnitPrice= decimal.Parse(reader2["UnitPrice"].ToString()!),
                    });
                }

                obj.TransactionProducts = list;

            }
            catch (Exception)
            {
                
            }
            return obj;
        }
        public async Task<MResponse> TransactionsCreate(MTransactions obj)
        {
            MResponse objRes = new MResponse();
            objRes.IsOk = false;
            string msg = "Transacción";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Transactions.sp_TransactionsCreate", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Date", obj.Date);
                command.Parameters.AddWithValue("@TransactionTypeId", obj.TransactionTypeId);
                command.Parameters.AddWithValue("@TotalPrice", obj.TotalPrice);
                command.Parameters.AddWithValue("@Detail", obj.Detail);


                await conn.OpenAsync();
                var scalar = await command.ExecuteScalarAsync();
                int id = int.Parse(scalar!.ToString()!);
                objRes.IsOk=id==0?false:true;
                objRes.Message = id.ToString();
               

            }
            catch (Exception)
            {
                msg = "Error interno, inténtelo más tarde.";
                objRes.IsOk = false;
                objRes.Message = msg;
            }

            return objRes;
        }
        public async Task<MResponse> TransactionsDelete(int Id)
        {
            MResponse objRes = new MResponse();
            string msg = "";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Transactions.sp_TransactionsDelete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", Id);

                await conn.OpenAsync();
                var scalar = await command.ExecuteScalarAsync();
                msg = scalar!.ToString()!;
                objRes.IsOk = true;
                objRes.Message = msg;
                if (msg.StartsWith("Error"))
                {
                    objRes.IsOk = false;

                }

            }
            catch (Exception)
            {
                msg = "Error interno, inténtelo más tarde.";
                objRes.IsOk = false;
                objRes.Message = msg;
            }

            return objRes;

        }
        public async Task<MResponse> TransactionsModify(MTransactions obj)
        {
            MResponse objRes = new MResponse();
            string msg = "";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Transactions.sp_TransactionsModify", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Date", obj.Date);
                command.Parameters.AddWithValue("@TransactionTypeId", obj.TransactionTypeId);
                command.Parameters.AddWithValue("@TotalPrice", obj.TotalPrice);
                command.Parameters.AddWithValue("@Detail", obj.Detail);
                command.Parameters.AddWithValue("@Id", obj.Id);

                await conn.OpenAsync();
                var scalar = await command.ExecuteScalarAsync();
                msg = scalar!.ToString()!;
                objRes.IsOk = true;
                objRes.Message = msg;
                if (msg.StartsWith("Error"))
                {
                    objRes.IsOk = false;

                }

            }
            catch (Exception)
            {
                msg = "Error interno, inténtelo más tarde.";
                objRes.IsOk = false;
                objRes.Message = msg;
            }

            return objRes;
        }
    }
}
