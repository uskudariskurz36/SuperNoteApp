using Microsoft.AspNetCore.Mvc;
using SuperNoteApp.Entities;
using SuperNoteApp.Helpers;
using SuperNoteApp.Models.NoteModels;

namespace SuperNoteApp.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Index(string sort = "desc")
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            NoteManager noteManager = new NoteManager();
            List<Note> notes = noteManager.GetNotesbyUserId(userid.Value);

            if (sort == "desc")
            {
                notes = (from n in notes
                         orderby n.CreatedDate descending
                         select n).ToList();
            }
            else
            {
                notes = (from n in notes
                         orderby n.CreatedDate ascending
                         select n).ToList();
            }

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
        public IActionResult Create(NoteCreateModel model)
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                NoteManager noteManager = new NoteManager();
                noteManager.CreateNote(userid.Value, model);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            NoteManager noteManager = new NoteManager();
            Note note = noteManager.GetNoteById(id);

            // Kayıt başkası tarafından silinmiş ise
            // note = null gelecek. Dolayısı ile Index e yönlendiririz.
            // Böylece veriler tekrar listelenir ve silinen kayıtlar gelmemiş olur.
            if (note == null)
            {
                return RedirectToAction(nameof(Index));
            }

            NoteEditModel model = new NoteEditModel()
            {
                Title = note.Title,
                Description = note.Description,
                IsDraft = note.IsDraft,
                IsPrivate = note.IsPrivate,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, NoteEditModel model)
        {
            if (ModelState.IsValid)
            {
                NoteManager noteManager = new NoteManager();
                noteManager.EditById(id, model);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Edit(id);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            NoteManager noteManager = new NoteManager();
            noteManager.RemoveById(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult GenerateData()
        {
            NoteManager noteManager = new NoteManager();
            noteManager.GenerateFakeData();

            return RedirectToAction(nameof(Index));
        }
    }
}
