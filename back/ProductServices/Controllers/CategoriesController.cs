using Microsoft.AspNetCore.Mvc;
using ProductServices.Models;
using ProductServices.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _CategoriesRepository;
        public CategoriesController(ICategoriesRepository CategoriesRepository)
        {
            _CategoriesRepository = CategoriesRepository;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<List<MCategories>> Get()
        {
            return await _CategoriesRepository.CategoriesList();
        }

    }
}
