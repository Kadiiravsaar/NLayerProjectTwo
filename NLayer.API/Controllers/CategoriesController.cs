using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        [HttpGet]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int id)
        {
            return Ok(await _categoryService.GetSingleCategoryByIdWithProductsAsync(id));

        }

       
    }
}
