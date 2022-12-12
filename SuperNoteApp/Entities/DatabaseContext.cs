using Microsoft.EntityFrameworkCore;

namespace SuperNoteApp.Entities
{
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
