﻿
using Car.Storage.Application.Administrators.Application.FluentValidators;
using FluentValidation.Results;

namespace Car.Storage.Application.Administrators.Application.BaseViewModel
{
    public class BaseViewModel
    {

        [System.Text.Json.Serialization.JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
              
        public void GenrateInvalidateViewModelResult(ValidationResult? validationResultWithError ,string errorMenssage)
        {
            if (validationResultWithError == null)
            {
                ValidationResult = new ValidationResult();
                ValidationResult.Errors.Add(new ValidationFailure("", $"{errorMenssage}"));
            }
            else
            {
                ValidationResult = new ValidationResult();
                ValidationResult.Errors.AddRange(validationResultWithError.Errors);
            }
        }

        public BaseViewModel GenerateValidViewModel(BaseViewModel baseViewModel)
        {
         
            var validator = new AlwaysValidValidator<BaseViewModel>();
            var viewModelValidationSucess = validator.Validate(baseViewModel);

            baseViewModel.ValidationResult = viewModelValidationSucess;

            return baseViewModel;
        }
       
    }
}
