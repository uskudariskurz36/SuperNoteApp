using System.ComponentModel.DataAnnotations;

namespace SuperNoteApp.Models.NoteModels
{
    public class NoteEditModel
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsDraft { get; set; }
        public bool IsPrivate { get; set; }
    }
}
