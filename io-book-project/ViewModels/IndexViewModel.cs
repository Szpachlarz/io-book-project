using io_book_project.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace io_book_project.ViewModels
{
    public class IndexViewModel
    {
        //public IEnumerable<Book> Books { get; set; }
        public SelectList? Categories { get; set; }
        public string? SelectedCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
