using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("[action]")] // action demek şu isimle  ProdWithCategory  istek yap demek
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return Ok(await _service.ProductsWithCategory());
        }



        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {

            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return Ok(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))] //  [ValidateFilter] yazarak kullanamazsın çünkü NotFoundFilter bir attrribute sınıfını miras almaz, validatefilter alır git bak

        [HttpGet("{id}")]
        // api/products/5
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return Ok(CustomResponseDto<ProductDto>.Success(200, productsDto));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(product);

            return Ok(CustomResponseDto<ProductDto>.Success(200));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return Ok(CustomResponseDto<ProductUpdateDto>.Success(204));
        }


    }
}
