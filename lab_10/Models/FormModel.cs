using lab_10.enums;
using lab_10.Validation;
using System.ComponentModel.DataAnnotations;


namespace lab_10.Models
{
    public class FormModel
    {
        [Required(ErrorMessage = "Не вказано ім`я")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Довжина строки повинна бути від 3 до 50 символів")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не вказана електрона адреса")]
        [EmailAddress(ErrorMessage = "Некорректна електрона адреса")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не вказана дата")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Дата має бути в майбутньому")]
        [NoConsultationOnMonday(ErrorMessage = "Консультації за продуктом 'Основи' не проводяться в понеділок")]
        public DateTime Data_consult { get; set; }

        [Required(ErrorMessage = "Будь ласка, оберіть тип консультації")]
        public ConsultationTypeEnum Type_consult { get; set; }
    }
}
