using Microsoft.AspNetCore.Mvc;
using SuperNoteApp.Entities;
using SuperNoteApp.Helpers;

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

            NoteManager noteManager = new NoteManager();
            List<Note> notes = noteManager.GetNotesbyUserId(userid.Value);

            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(string s)
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Insert kodları gelecek.

            return View();
        }

        public IActionResult GenerateData()
        {
            NoteManager noteManager = new NoteManager();
            noteManager.GenerateFakeData();

            return RedirectToAction(nameof(Index));
        }
    }
}
