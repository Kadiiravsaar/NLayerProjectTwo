using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;
using System.Collections.Generic;

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

        [HttpGet("ProdWithCategory")]
        public async Task<IActionResult> ProdWithCategory()
        {
            return Ok(await _service.ProductsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
          
            var products = await _service.GetAllAsync();
            if (products.Count()<0)
            {
                return Ok(MessageDto<NoContentDto>.Message("Böyle bir şey yok arkadaş", 204));

            }
            var prodsDto = _mapper.Map<List<ProductDto>>(products.ToList());
            return Ok(CustomResponseDto<List<ProductDto>>.Success(200, prodsDto));
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            var products = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var prodsDto = _mapper.Map<ProductDto>(products);
            return Ok(CustomResponseDto<ProductDto>.Success(200, prodsDto));
        }

        [HttpGet("{id}")]
        // api/products/5
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(CustomResponseDto<ProductDto>.Success(200, productDto));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return Ok(CustomResponseDto<ProductDto>.Success(200));
        }

        [HttpPut("UpdateProd")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return Ok(CustomResponseDto<ProductUpdateDto>.Success(204));
        }



    }
}
