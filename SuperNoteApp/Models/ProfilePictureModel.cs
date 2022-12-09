using System.ComponentModel.DataAnnotations;

namespace SuperNoteApp.Models
{
    public class ProfilePictureModel
    {
        [Required]
        public IFormFile NewPicture { get; set; }
    }
}
