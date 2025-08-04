using Microsoft.Data.SqlClient;
using System.Data;
using TransactionServices.DB;
using TransactionServices.Models;

namespace TransactionServices.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly string ConnectionString = "";
        public TransactionTypeRepository(Connection _configuration)
        {
            ConnectionString = _configuration.ConnectionString;
        }

        public async Task<List<MTransactionTypes>> TransactionTypesList()
        {
            List<MTransactionTypes> TList = new List<MTransactionTypes>();
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Transactions.sp_TransactionTypesList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    TList.Add(new MTransactionTypes
                    {
                        Id = int.Parse(reader["Id"].ToString()!),
                        Name = reader["Name"].ToString()!,
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
    }
}
