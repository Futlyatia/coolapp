using System.ComponentModel.DataAnnotations;

namespace coolapp.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите никнейм")]
        [Display(Name = "никнейм")]
        [DataType(DataType.Text)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}