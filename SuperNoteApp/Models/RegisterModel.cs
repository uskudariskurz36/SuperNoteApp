using System.ComponentModel.DataAnnotations;

namespace SuperNoteApp.Models
{
    public class RegisterModel : LoginModel
    {
        [Display(Name = "Şifre(Tekrar)")]
        [Required(ErrorMessage = "Şifre(Tekrar) boş geçilemez.")]
        [MinLength(6, ErrorMessage = "Şifre(tekar) en az 6 kadarkter olmalıdır.")]
        [MaxLength(16, ErrorMessage = "Şifre(tekar) en fazla 16 kadarkter olmalıdır.")]
        [Compare("Password", ErrorMessage = "Şifre ile şifre(tekrar) uyuşmuyor.")]
        public string RePassword { get; set; }
    }
}
