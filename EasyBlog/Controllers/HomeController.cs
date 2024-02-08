using EasyBlog.Data;
using EasyBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EasyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int cid = 0)
        {
            if(cid == 0)
            {
                return View(_db.Posts.OrderByDescending(x => x.Id).ToList());
            }
            else
            {   
                ViewBag.Category = _db.Categories.Find(cid).Name;

                if(ViewBag.Category == null) return NotFound();

                ViewBag.Number = cid;

                return View(_db.Posts.OrderByDescending(_ => _.Id).Where(x => x.CategoryId == cid).ToList());
            }
        }

        [Route("Post/{id:int}")]
        public IActionResult ShowPost(int id)
        {
            Post? post = _db.Posts
                .Include(x => x.Category)
                .FirstOrDefault(x =>x.Id == id);

            if (post == null) return NotFound();

            return View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
