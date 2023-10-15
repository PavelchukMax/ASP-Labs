using System.ComponentModel.DataAnnotations;

namespace lab_6.ViewModels
{
    public record class UserV(
                              [Required] string Name, 
                              [Required] string Passw, 
                              [Required][Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")] string Age);
}
