using System.ComponentModel.DataAnnotations;

namespace coolapp.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите E-mail")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]   // тип элемента управления на странице
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите никнейм")]
        [Display(Name = "Никнейм")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]   // тип элемента управления на странице
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите ввод пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]   // механизм, который сверяет текущее значение PasswordConfirm, с указанным Password
        [DataType(DataType.Password)]   // тип элемента управления на странице
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

    }
}