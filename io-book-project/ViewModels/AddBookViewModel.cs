using System.ComponentModel.DataAnnotations;

namespace io_book_project.ViewModels
{
    public class AddBookViewModel
    {
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Nazwa książki jest wymagana")]
        [StringLength(120, ErrorMessage = "Maksymalnie 120 znaków")]
        public string Title { get; set; }
    }
}
