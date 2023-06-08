using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var category = await _categoryService.GetAllAsync();

            var categoryDto = _mapper.Map<List<CategoryDto>>(category.ToList());

            return Ok(CustomResponseDto<List<CategoryDto>>.Success(200, categoryDto));
        }


        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            var addcCategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

            var mapCategoryDto = _mapper.Map<CategoryDto>(addcCategory);

            return Ok(CustomResponseDto<CategoryDto>.Success(200, mapCategoryDto));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var findCategory = await _categoryService.GetByIdAsync(id);

            await _categoryService.RemoveAsync(findCategory);

            return Ok(CustomResponseDto<CategoryDto>.Success(200));
        }


        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryDto));

            return Ok(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpGet("{id}")] 
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var getIdByCategory = await _categoryService.GetByIdAsync(id);

            if (getIdByCategory == null)
            {
                return Ok(MessageDto<NoContentDto>.Message("Böyle id'ye sahip kategori yok"));
            }

            var categoryMap = _mapper.Map<CategoryDto>(getIdByCategory);

            return Ok(CustomResponseDto<CategoryDto>.Success(200, categoryMap));
        }


        [HttpGet("GetSingleCategoryByIdWithProducts")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int id)
        {
            return Ok(await _categoryService.GetSingleCategoryByIdWithProductsAsync(id));
        }


    }
}
