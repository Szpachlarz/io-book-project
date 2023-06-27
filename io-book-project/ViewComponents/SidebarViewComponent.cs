using io_book_project.Interfaces;
using io_book_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace io_book_project.ViewComponents
{
    public class SidebarViewComponent:ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public SidebarViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAll();
            return View(categories);
            //var indexVM = new IndexViewModel
            //{
            //    Categories = new SelectList(await _categoryRepository.GetAll())
            //};

            //return View(indexVM);
        }
    }
}
