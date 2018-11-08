using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Client.Web.Models;

namespace TimeSheet.Client.Web.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            ErrorViewModel errorVM = new ErrorViewModel();
            return View("Error", errorVM);
        }
    }
}