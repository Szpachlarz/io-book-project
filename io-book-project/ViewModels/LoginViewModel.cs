using System.ComponentModel.DataAnnotations;

namespace io_book_project.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Adres E-mail")]
        [Required(ErrorMessage = "Adres E-mail jest wymagany")]
        public string EmailAddress { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
