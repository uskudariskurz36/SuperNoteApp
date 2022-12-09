using Microsoft.EntityFrameworkCore;
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
        public DateTime CreatedDate { get; set; }
        
        // Foreign Key kolon için.
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Z36NoteApp2DB;Trusted_Connection=true");
            }
        }
    }
}
