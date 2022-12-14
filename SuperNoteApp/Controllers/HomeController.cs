using Microsoft.AspNetCore.Mvc;
using SuperNoteApp.Entities;
using SuperNoteApp.Helpers;
using SuperNoteApp.Models;
using System.Diagnostics;

namespace SuperNoteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? userid = null)
        {
            NoteManager noteManager = new NoteManager();
            List<Note> notes = null;

            if (userid == null)
            {
                notes = noteManager.GetNotesByNonPrivate();
            }
            else
            {
                notes = noteManager.GetNotesByNonPrivateAndUserId(userid.Value);
            }

            return View(notes);
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