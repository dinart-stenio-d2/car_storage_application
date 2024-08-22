using FluentValidation;
using FluentValidation.Results;

namespace Car.Storage.Application.SharedKernel.ValueObjects
{
    public abstract class ValueObjectsBase
    {
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// Validate business rules of the instance using fluent validations
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="validator"></param>
        public async void ValidateAsync<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = await validator.ValidateAsync(model);
        }
    }
}
