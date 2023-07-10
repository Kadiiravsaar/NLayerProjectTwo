namespace NLayer.Core.DTOs
{
    public class CategoryWithProductsDto : CategoryDto
    {
        public List<ProductDto> Product { get; set; }
    }
}
