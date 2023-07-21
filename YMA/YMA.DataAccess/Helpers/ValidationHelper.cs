using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.Entities.Models;

namespace YMA.DataAccess.Helpers
{
    public class ValidationHelper<T>
    {
        private readonly IValidator<T> _validator;

        public ValidationHelper(IValidator<T> validator)
        {
            _validator = validator;
        }

        public ResponseModel Validate(T entity)
        {
            ValidationResult validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return new ResponseModel()
                {
                    message = validationResult.Errors.First().ErrorMessage,
                };
            }
            return new ResponseModel()
            {
                status_code = StatusCodes.Status200OK,
            };
        }
    }
}
