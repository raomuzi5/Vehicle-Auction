using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Proji.Domain.Validators
{
    public class GenericValidator<T> : AbstractValidator<T>
    {
        public GenericValidator()
        {
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(ValidationAttribute), true)
                                         .Cast<ValidationAttribute>()
                                         .ToList();

                foreach (var attribute in attributes)
                {
                    RuleFor(x => property.GetValue(x))
                        .Must((x, value) =>
                        {
                            var validationContext = new ValidationContext(x) { MemberName = property.Name };
                            var result = attribute.GetValidationResult(value, validationContext);
                            return result == ValidationResult.Success;
                        })
                        .WithMessage(attribute.ErrorMessage);
                }
            }
        }
    }
}
