using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Module5Demo.Validation
{
    public class CustomValidationMetadataProvider : IValidationMetadataProvider
    {
        Dictionary<Type, string> _errorMessageMap;

        public CustomValidationMetadataProvider()
        {
            _errorMessageMap = new Dictionary<Type, string>
            {
                { typeof(RequiredAttribute), "RequiredError" },
                { typeof(MaxLengthAttribute), "MaxLengthError" }
            };
        }
        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            foreach(object attribute in context.Attributes)
            {
                ValidationAttribute validationAttribute =
                    attribute as ValidationAttribute;

                if(validationAttribute != null)
                {
                    Type type = attribute.GetType();
                    string key;
                    if(_errorMessageMap.TryGetValue(type, out key))
                    {
                        validationAttribute.ErrorMessage = key;
                    }
                }
            }
        }
    }
}
