using lab_10.enums;
using System.ComponentModel.DataAnnotations;

namespace lab_10.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var selectedDate = (DateTime)value;
            if (selectedDate > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }

    public class NoConsultationOnMondayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var selectedDate = (DateTime)value;
            var selectedConsult = (ConsultationTypeEnum)validationContext.ObjectInstance.GetType()
                .GetProperty("Type_consult")
                .GetValue(validationContext.ObjectInstance);

            if (selectedDate.DayOfWeek != DayOfWeek.Monday || selectedConsult != ConsultationTypeEnum.Basics)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }


}
