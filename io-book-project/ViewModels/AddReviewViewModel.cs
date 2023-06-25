using System.ComponentModel.DataAnnotations;

namespace io_book_project.ViewModels
{
    public class AddReviewViewModel
    {
        [Required(ErrorMessage = "Recenzja jest wymagana")]
        [StringLength(500, ErrorMessage = "Maksymalnie 500 znaków")]
        public string Text { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
