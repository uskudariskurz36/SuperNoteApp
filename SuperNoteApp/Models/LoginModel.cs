using System.ComponentModel.DataAnnotations;

namespace SuperNoteApp.Models
{
    public class LoginModel
    {

        // Data Annotations
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez.")]
        public string Username { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter olmalıdır.")]
        public string Password { get; set; }
    }
}
