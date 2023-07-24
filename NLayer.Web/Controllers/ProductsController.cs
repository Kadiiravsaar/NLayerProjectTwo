using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, ICategoryService categoryService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _service.ProductsWithCategory());
        }


        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<Category>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryDto, "Id", "Name"); // dropdownList bu 1. Bana bir liste ver(categories)- 2. dropdan bir şey seçildiğinde ben ıd göstericem. - 3. kullanıcılar neyi görecek (name)   
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            if (ModelState.IsValid) // name price stock geçerli ise eyvalla gel
            {
                await _service.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<Category>>(categories.ToList());
            ViewBag.categories = new SelectList(categoryDto, "Id", "Name"); // dropdownList bu 1. Bana bir liste ver(categories)- 2. dropdan bir şey seçildiğinde ben ıd göstericem. - 3. kullanıcılar neyi görecek (name)   
            return View();

        }


        [HttpGet]
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var prod = await _service.GetByIdAsync(id);

            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<Category>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryDto, "Id", "Name", prod.CategoryId); 
            return View(_mapper.Map<ProductDto>(prod));


        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<Category>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryDto, "Id", "Name",productDto.CategoryId);
            return View();


        }


        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var prod = await _service.GetByIdAsync(id);
            if (ModelState.IsValid)
            {

                await _service.RemoveAsync(prod);
                return RedirectToAction(nameof(Index));
            }
            return View();


        }
    }
}

