using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ND_web.Models;
using Newtonsoft.Json;

namespace ND_web
{
    public class RegisterBindingModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите имя, пожалуйста")]
        [StringLength(23, ErrorMessage = "Слишком короткое/длинное имя", MinimumLength = 5)]
        public string ProfileName { get; set; }

        [Required(ErrorMessage = "Введите email, пожалуйста!")]
        [EmailAddress(ErrorMessage = "Введите корректный email, пожалуйста")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль, пожалуйста")]
        [StringLength(100, ErrorMessage = "Должно быть максимум {0}, минимум {2} символов.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль, пожалуйста")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required(ErrorMessage = "Введите текущий пароль, пожалуйста")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль, пожалуйста")]
        [StringLength(100, ErrorMessage = "Должно быть максимум {0}, минимум {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Подтвердите новый пароль, пожалуйста")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required(ErrorMessage = "Введите новый пароль, пожалуйста")]
        [StringLength(100, ErrorMessage = "Должно быть максимум {0}, минимум {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Подтвердите новый пароль, пожалуйста")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль, пожалуйста")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }

    public class LoginBindingModel
    {
        [Required(ErrorMessage = "Введите email, пожалуйста!")]
        [EmailAddress(ErrorMessage = "Введите корректный email, пожалуйста")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль, пожалуйста")]
        [StringLength(100, ErrorMessage = "Должно быть максимум {0}, минимум {2} символов.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string UserId { get; set; }
    }

}
