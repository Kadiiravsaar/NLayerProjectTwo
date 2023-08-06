using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));


            }

            #region Filter
            //Filter => controllerlara gelen isteğe müdahale etmek için kullanıyoruz
            //gelmeden Önce geldikden sonra gibi işlemler yapabiliyoruz müdahale ediyoruz
            //eğer validation hatası varsa kendi custom modelimizi dönmemizi sağlayacak daha controllera girmeden

            #endregion

        }
    }
}
