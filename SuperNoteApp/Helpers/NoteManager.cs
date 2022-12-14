using MFramework.Services.FakeData;
using Microsoft.EntityFrameworkCore;
using SuperNoteApp.Entities;
using SuperNoteApp.Models.NoteModels;

namespace SuperNoteApp.Helpers
{
    public class NoteManager
    {
        private DatabaseContext db = new DatabaseContext();

        public List<Note> GetNotesbyUserId(int userId)
        {
            // Lambda expression
            List<Note> notes = db.Notes.Where(n => n.UserId == userId).ToList();


            // Select * From Notes Where UserId=@userId
            //List<Note> notes = (from n in db.Notes
            //                    where n.UserId == userId
            //                    select n).ToList();


            return notes;
        }

        public void GenerateFakeData()
        {
            // Any metodu bize true ya da false döner.
            // Eğer notes tablosunda kayıt varsa true yoksa false döner. 
            bool hasNote = db.Notes.Any();

            // Note varsa metot dan çık aşağıdakileri insert etme.
            if (hasNote) return;

            // Select Id From Users
            List<int> userIds = (from u in db.Users
                                 select u.Id).ToList();

            for (int i = 0; i < 50; i++)
            {
                Note note = new Note();
                note.Title = NameData.GetCompanyName();
                note.Description = TextData.GetSentence();
                note.IsDraft = BooleanData.GetBoolean();
                note.CreatedDate = DateTimeData.GetDatetime();
                note.UserId = CollectionData.GetElement(userIds);

                db.Notes.Add(note);
            }

            db.SaveChanges();
        }

        public void CreateNote(int userId, NoteCreateModel model)
        {
            Note note = new Note()
            {
                Title = model.Title,
                Description = model.Description,
                IsDraft = model.IsDraft,
                IsPrivate = model.IsPrivate,
                CreatedDate = DateTime.Now,
                UserId = userId,
            };

            db.Notes.Add(note);
            db.SaveChanges();
        }

        public Note GetNoteById(int id)
        {
            Note note = db.Notes.Where(n => n.Id == id).FirstOrDefault();

            //Note note = (from n in db.Notes
            //             where n.Id == id
            //             select n).FirstOrDefault();

            return note;
        }

        public void EditById(int id, NoteEditModel model)
        {
            Note note = GetNoteById(id);

            if (note == null)
            {
                return;
            }

            note.Title = model.Title;
            note.Description = model.Description;
            note.IsDraft = model.IsDraft;
            note.IsPrivate = model.IsPrivate;
            note.ModifiedDate= DateTime.Now;

            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Note note = GetNoteById(id);

            if (note != null)
            {
                db.Notes.Remove(note);
                db.SaveChanges();
            }
        }

        public List<Note> GetNotesByNonPrivate()
        {
            List<Note> notes = db.Notes
                .Where(n => !n.IsPrivate)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();

            //List <Note> notes = (from n in db.Notes.Include("User")
            //                    where n.IsPrivate == false
            //                    orderby n.CreatedDate descending
            //                    select n).ToList();

            return notes;
        }

        public List<Note> GetNotesByNonPrivateAndUserId(int userId)
        {
            List<Note> notes = db.Notes
                .Where(n => !n.IsPrivate && n.UserId == userId)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();

            //List<Note> notes = (from n in db.Notes.Include("User")
            //                    where n.IsPrivate == false && n.UserId == userId
            //                    orderby n.CreatedDate descending
            //                    select n).ToList();

            return notes;
        }
    }
}
