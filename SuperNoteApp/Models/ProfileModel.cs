using System.ComponentModel.DataAnnotations;

namespace SuperNoteApp.Models
{
    public class ProfileModel
    {
        [Display(Name = "Ad")]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(50)]
        public string? Surname { get; set; }

        public bool IsUpdatePassword { get; set; }

        [Display(Name = "Şifre")]
        [MinLength(6), MaxLength(16)]
        public string? Password { get; set; }

        [Display(Name = "Şifre(Tekrar)")]
        [MinLength(6), MaxLength(16)]
        [Compare(nameof(Password))]
        public string? RePassword { get; set; }
    }
}
