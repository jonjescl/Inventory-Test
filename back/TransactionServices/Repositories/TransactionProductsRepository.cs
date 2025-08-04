using Microsoft.Data.SqlClient;
using System.Data;
using TransactionServices.DB;
using TransactionServices.Models;

namespace TransactionServices.Repositories
{
    public class TransactionProductsRepository : ITransactionProductsRepository
    {
        private readonly string ConnectionString = "";
        public TransactionProductsRepository(Connection _configuration)
        {
            ConnectionString = _configuration.ConnectionString;
        }

        public async Task<MResponse> TransactionProductsCreate(int IdTransaction, MTransactionProducts obj)
        {
            MResponse objRes = new MResponse();
            string msg = "";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Transactions.sp_TransactionProductCreate", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@TransactionId", IdTransaction);
                command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                command.Parameters.AddWithValue("@ProductName", obj.ProductName);
                command.Parameters.AddWithValue("@Quantity", obj.Quantity);
                command.Parameters.AddWithValue("@UnitPrice", obj.UnitPrice);
                command.Parameters.AddWithValue("@TotalPrice", obj.TotalPrice);

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
