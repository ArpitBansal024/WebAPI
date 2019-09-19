using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.CustomAttributes
{
    public class EmailValidationAttribute: ValidationAttribute
    {
        private const string VALIDATION_TYPE = "customEmail";
        private const string EMAIL_REGEX = @"put your regex here";

        public virtual IEnumerable<ModelClientValidationRule> GetClientValidationRules(System.Web.Http.Metadata.ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule { ValidationType = VALIDATION_TYPE, ErrorMessage = ErrorMessageString };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var sValue = value as string;

            if (!string.IsNullOrEmpty(sValue) && Regex.Match(sValue, EMAIL_REGEX).Success)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format(ErrorMessageString, validationContext.MemberName));
        }
    }
}