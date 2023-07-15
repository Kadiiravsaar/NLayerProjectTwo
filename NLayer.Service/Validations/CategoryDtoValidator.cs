using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validations
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");

        }
    }
}
