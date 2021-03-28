using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceChat.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            ViewBag.Title = "Really Task Controller!";
            return View();
        }
    }
}