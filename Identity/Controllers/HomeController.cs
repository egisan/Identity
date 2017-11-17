using Identity.Models;
using System.Web.Mvc;

namespace Identity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Index (Public)";
            return View();
        }

        public ActionResult Default()
        {
            ViewBag.Title = "Default";
            return View("Index");
        }

        [Authorize(Roles = Role.Admin)]
        public ActionResult Admin()
        {
            ViewBag.Title = "Admin";
            return View("Index");
        }

        [Authorize(Roles = Role.Editor)]
        public ActionResult Editor()
        {
            ViewBag.Title = "Editor";
            return View("Index");
        }

        [Authorize(Roles = Role.AdminOrEditor)]
        public ActionResult AdminOrEditor()
        {
            ViewBag.Title = "Admin Or Editor";
            return View("Index");
        }

        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Editor)]
        public ActionResult AdminAndEditor()
        {
            ViewBag.Title = "Admin Or Editor";
            return View("Index");
        }

        [Authorize(Users = "user@lexicon.se")]
        public ActionResult UserIdentity()
        {
            ViewBag.Title = "User Identity";
            return View("Index");
        }



    }
}