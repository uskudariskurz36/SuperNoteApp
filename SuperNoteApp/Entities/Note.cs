using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperNoteApp.Entities
{
    [Table("Notes")]
    public class Note
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsDraft { get; set; }

        public bool IsPrivate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // Foreign Key kolon için.
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
