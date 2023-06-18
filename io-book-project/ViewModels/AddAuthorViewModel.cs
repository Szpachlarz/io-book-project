using io_book_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace io_book_project.ViewModels
{
    public class AddAuthorViewModel
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa autora jest wymagana")]
        [StringLength(120, ErrorMessage = "Maksymalnie 120 znaków")]
        public string Names { get; set; }
        public string? Surname { get; set; }

        public string Nationality { get; set; }

        public string DateOfBirth { get; set; }

        public string? DateOfDeath { get; set; }
    }

}

