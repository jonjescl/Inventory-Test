using Microsoft.Data.SqlClient;
using ProductServices.DB;
using ProductServices.Models;
using System.Data;

namespace ProductServices.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly string ConnectionString = "";
        public CategoriesRepository(Connection _configuration)
        {
            ConnectionString = _configuration.ConnectionString;
        }
        public async Task<List<MCategories>> CategoriesList()
        {
            List<MCategories> CList = new List<MCategories>();
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Products.sp_CategoriesList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    CList.Add(new MCategories
                    {
                        Id = int.Parse(reader["Id"].ToString()!),
                        Name = reader["Name"].ToString()!,
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()!),
                        UpdatedAt= DateTime.Parse(reader["UpdatedAt"].ToString()!),
                    });
                }
            }
            catch (Exception)
            {

            }
            return CList;
        }

    }
}
