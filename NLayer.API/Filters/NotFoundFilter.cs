using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;


namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        #region Açıklama
        // Service katmanından fırlatılan exception globaldi
        // bazen öyle bi durum olur ki data null olursa durum değişmelidir
        // farklı durumlar için bi filter yazmamız gerekebilir bunun gibi.
        // daha controllera girmden bunu yapacağız

        #endregion
        // amacım şu => daha controllera gelmeden kontrol et
        private readonly IService<T> _service;
        // eğer bir filer ctorda servici veya classı DI olarak geçerse bunu program.cs eklemen lazım
        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var idValue = context.ActionArguments.Values.FirstOrDefault(); // controllra gelecek olan ıd yakalanacak(controllrda bulunan propda ki ilk değeri al )

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue; // id var ise cast edip atama yapıyoruz
            var anyEntity = await _service.AnyAsync(x => x.Id == id); // entity var mı onu kontrol ediyoruz. Bunun için service katmanını çağırıp anyi çağırdık
            // id gelmeyebilir çünkü BaseEntity tanımlamak gerekir çünkü Id prop orda var
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));
            // kendi dto'muzu döndük
        }
    }
}

