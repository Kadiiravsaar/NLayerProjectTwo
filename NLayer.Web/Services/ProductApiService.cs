using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductWithCategoryDto()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("ProdWithCategory");
            return response.Data;

            // GetFromJsonAsync direk datayı json olarak almamızı sağlar

            // nasıl bir data bekliyorum sorusunun cevabı
            // <Apiden o controllerda nasıl beklediğine bağlı (<CustomResponseDto<List<ProductWithCategoryDto>>>)> bu beklemiş olduğun tiptir

            // nereye istek yapmak istiyorsun bunu veriyor <>(Burada) ("ProdWithCategory")
        }


        public async Task<ProductDto> Save(ProductDto newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("addProduct", newProduct);

            if (!response.IsSuccessStatusCode) return null; // burayı geçerse tamam demektir

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();

            return responseBody.Data;
        }
    }
}
