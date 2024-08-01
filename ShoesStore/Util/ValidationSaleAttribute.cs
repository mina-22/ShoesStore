using ShoesStore.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Util
{
    public class ValidationSaleAttribute() : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            float price;
            Product p = (Product)validationContext.ObjectInstance;
            if(!float.TryParse(value.ToString() , out  price))
            {
                return new ValidationResult("Enter valid number");
            }
            if(p.Sale >= p.Price)
            {
                return new ValidationResult("Invalid sale price , should be less than main Price");
            }
            return ValidationResult.Success;
        }
    }
}
