using EasyBlog.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CategoryViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {          
            return View(_db.Categories.ToList());
        }
    }
}
