using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("Categories/GetAllCategories");
            return response.Data;
        }

        public async Task<CategoryDto> Save(CategoryDto newCat)
        {
            var response = await _httpClient.PostAsJsonAsync("Categories/addCategory",newCat);
            if (!response.IsSuccessStatusCode) return null;

        
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<CategoryDto>>();
            return responseBody.Data;
        }
    }
}
