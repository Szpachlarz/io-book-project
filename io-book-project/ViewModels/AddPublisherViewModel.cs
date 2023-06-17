using io_book_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace io_book_project.ViewModels
{
    public class AddPublisherViewModel
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(120, ErrorMessage = "Maksymalnie 120 znaków")]
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
