using EasyBlog.Areas.Admin.Models;
using EasyBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
        private readonly ApplicationDbContext _db;

        public PostsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var posts = _db.Posts.Include(x => x.Category).OrderByDescending(u => u.Id).ToList();
            return View(posts);
        }

        public IActionResult Create()
        {
            LoadSelectCategories();
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostViewModel vm)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            LoadSelectCategories();
            return View();
        }

        private void LoadSelectCategories()
        {
            ViewBag.Categories = _db.Categories
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToList();
        }
    }
}
