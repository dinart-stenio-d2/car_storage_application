using FluentValidation;
using FluentValidation.Results;
using System;
using System.Runtime.InteropServices;

namespace Car.Storage.Application.SharedKernel.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public ValidationResult ValidationResult { get; set; }

        protected Entity([Optional] string idParam)
        {
            if (string.IsNullOrEmpty(idParam))
            {
                Id = Guid.NewGuid();
            }
            else
            {
                Id = new Guid(idParam);
            }
        }

        /// <summary>
        /// Method used to compare instances of a class
        /// Each entity have own identity so for one entity
        /// to be equal to another it need to be of the same type 
        /// and also have the same Id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true or false</returns>
        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }


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
