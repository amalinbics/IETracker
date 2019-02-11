using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IETracker.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult Add(int id =0 )
        {
            ViewBag.Id = id;
            return View("frmTransaction");
        }

    }
}