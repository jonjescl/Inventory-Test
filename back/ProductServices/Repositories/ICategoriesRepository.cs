using ProductServices.Models;

namespace ProductServices.Repositories
{
    public interface ICategoriesRepository
    {
        Task<List<MCategories>> CategoriesList();
    }
}
