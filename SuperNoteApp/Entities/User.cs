using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperNoteApp.Entities
{
    [Table("Users")]
    public class User
    {
        //[Column("UserId")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25)]
        public string? Name { get; set; }

        [StringLength(30)]
        public string? Surname { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        [StringLength(25)]
        public string Password { get; set; }

        [Required]
        [StringLength(25)]
        [DefaultValue("default.png")]
        public string Picture { get; set; }

        // Navigation property
        public List<Note> Notes { get; set; }
    }
}
