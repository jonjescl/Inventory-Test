using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using ProductServices.DB;
using ProductServices.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace ProductServices.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string ConnectionString = "";
        public ProductsRepository(Connection _configuration)
        {
            ConnectionString = _configuration.ConnectionString;
        }
        public async Task<List<MProducts>> ProductsList()
        {
            List<MProducts> PList = new List<MProducts>();
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Products.sp_ProductsList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await conn.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    PList.Add(new MProducts
                    {
                        Id = int.Parse(reader["Id"].ToString()!),
                        Name = reader["Name"].ToString()!,
                        Description = reader["Description"].ToString()!,
                        Price=decimal.Parse(reader["Price"].ToString()!),
                        Stock= int.Parse(reader["Stock"].ToString()!),
                        Image= reader["Image"].ToString()!,
                        CategoryId = int.Parse(reader["CategoryId"].ToString()!),
                        Category = new MCategories
                        {
                            Id = int.Parse(reader["CategoryId"].ToString()!),
                            Name = reader["CategoryName"].ToString()!,
                            CreatedAt = DateTime.Parse(reader["CreatedAtCategory"].ToString()!),
                            UpdatedAt = DateTime.Parse(reader["UpdatedAtCategory"].ToString()!),
                        },
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()!),
                        UpdatedAt= DateTime.Parse(reader["UpdatedAt"].ToString()!),
                    });
                }
            }
            catch (Exception)
            {

            }
            return PList;
        }
        public async Task<MResponse> ProductsCreate(MProducts obj)
        {
            MResponse objRes = new MResponse();
            string msg = "";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Products.sp_ProductsCreate", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Price", obj.Price);
                command.Parameters.AddWithValue("@CategoryId", obj.CategoryId);
                command.Parameters.AddWithValue("@Description", obj.Description);
                command.Parameters.AddWithValue("@Stock", obj.Stock);
                command.Parameters.AddWithValue("@Image", obj.Image);

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

        public async Task<MResponse> ProductsDelete(int Id)
        {
            MResponse objRes = new MResponse();
            string msg = "";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Products.sp_ProductsDelete", conn)
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
        public async Task<MResponse> ProductsModify(MProducts obj)
        {
            MResponse objRes = new MResponse();
            string msg = "";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Products.sp_ProductsModify", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Price", obj.Price);
                command.Parameters.AddWithValue("@CategoryId", obj.CategoryId);
                command.Parameters.AddWithValue("@Description", obj.Description);
                command.Parameters.AddWithValue("@Stock", obj.Stock);
                command.Parameters.AddWithValue("@Image", obj.Image);
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

        public async Task<MResponse> ProductsStock(int Id, int Quantity)
        {
            MResponse objRes = new MResponse();
            string msg = "";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("Products.sp_ProductStock", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Quantity", Quantity);

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
