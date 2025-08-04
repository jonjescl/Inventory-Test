using ProductServices.Models;

namespace ProductServices.Repositories
{
    public interface IProductsRepository
    {
        Task<List<MProducts>> ProductsList();
        Task<MResponse> ProductsCreate(MProducts obj);
        Task<MResponse> ProductsModify(MProducts obj);
        Task<MResponse> ProductsStock(int Id, int Quantity);
        Task<MResponse> ProductsDelete(int Id);
    }
}
