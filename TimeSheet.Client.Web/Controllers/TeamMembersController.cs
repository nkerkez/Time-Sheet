using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Client.Web.Models.TimeSheet;

namespace TimeSheet.Client.Web.Controllers
{
    public class TeamMembersController : Controller
    {
        // GET: TeamMembers
        public ActionResult Index()
        {
            return View(new TimeSheetViewModel { MenuItemName = "TeamMember"});
        }
    }
}