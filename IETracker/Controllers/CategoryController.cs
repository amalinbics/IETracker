using IETracker.DTO;
using IETracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IETracker.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Save(int id = 0)
        {
            ViewBag.id = id;
            return View("CategoryForm");
        }
    }
}