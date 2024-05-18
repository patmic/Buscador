using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Validation
{
    public class RequiredClaveAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var idUsuarioProperty = instance.GetType().GetProperty("IdUsuario");

            if (idUsuarioProperty != null)
            {
                var idUsuarioValue = idUsuarioProperty.GetValue(instance);

                if ((idUsuarioValue == null || (int)idUsuarioValue == 0) && string.IsNullOrEmpty(value?.ToString()))
                {
                    return new ValidationResult(ErrorMessage ?? "Campo Clave es obligatorio");
                }
            }

            return ValidationResult.Success;
        }
    }
}
