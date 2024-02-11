using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SimpleTask.Api.Attiebutes
{
    public class AllowedExtensionsAttributes : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttributes(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            var Files = value as List<IFormFile>;

            if (Files != null && Files.Count != 0)
            {
                bool isvalid = true;

                Files.ForEach(file =>
                {
                    var Extension = Path.GetExtension(file.FileName);
                    var isAllowed = _allowedExtensions.Split(",").Contains(Extension, StringComparer.OrdinalIgnoreCase);
                    if (!isAllowed)
                    {
                        isvalid = false;
                        return;
                    }
                });
                if (!isvalid)
                {
                    return new ValidationResult($"Only {_allowedExtensions} are Allowed");
                }
            }
            return ValidationResult.Success;
        }
    }
}