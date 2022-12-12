using MFramework.Services.FakeData;
using SuperNoteApp.Entities;

namespace SuperNoteApp.Helpers
{
    public class NoteManager
    {
        private DatabaseContext db = new DatabaseContext();

        public List<Note> GetNotesbyUserId(int userId)
        {
            // Select * From Notes Where UserId=@userId
            List<Note> notes = (from n in db.Notes
                               where n.UserId == userId
                               select n).ToList();

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
    }
}
