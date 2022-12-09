using Microsoft.AspNetCore.Mvc;

namespace SuperNoteApp.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Index()
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}
