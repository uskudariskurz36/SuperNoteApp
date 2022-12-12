using SuperNoteApp.Entities;

namespace SuperNoteApp.Helpers
{
    public class NoteManager
    {
        private DatabaseContext db = new DatabaseContext();

        public List<Note> GetNotesbyUserId(int userId)
        {
            List<Note> notes = (from n in db.Notes
                               where n.UserId == userId
                               select n).ToList();

            return notes;
        }
    }
}
