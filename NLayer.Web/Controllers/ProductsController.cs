using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService service , ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ProductsWithCategory());
        }

        public async Task<IActionResult> Save(Product product)
        {
            return View(await _service.AddAsync(product));
            
        }
    }
}
