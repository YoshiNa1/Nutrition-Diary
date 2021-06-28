using System;
using System.Diagnostics;
using System.Net;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Services;
using NutritionDiary_app.Validators;
using NutritionDiary_app.Validators.Rules;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NutritionDiary_app.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private readonly ApiService service = new ApiService();

        private ValidatableObject<string> vemail;

        private string email;
        public string Email
        {
            get { return email;  }
            set {
                if (email != value)
                {
                    email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        public bool IsInvalidEmail { get; set; }

        #endregion

        public LoginViewModel()
        {
            Email = Settings.Email;
            Password = Settings.Password;
            InitializeProperties();
            AddValidationRules();
        }


        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var token = await service.LoginAsync(Email, Password);
                    Settings.AccessToken = token;

                    try { await service.GetProfileAsync(Settings.AccessToken); }
                    catch (NullReferenceException ex) { Debug.WriteLine(ex.Message); }

                    //try { await service.GetProfileAsync(Settings.AccessToken); }
                    //catch (WebException ex) { Debug.WriteLine(ex.Message); }
                });
            }
        }


        public ValidatableObject<string> VEmail
        {
            get
            {
                return vemail;
            }

            set
            {
                if (vemail == value)
                {
                    return;
                }

                SetProperty(ref vemail, value);
            }
        }

        /// <summary>
        /// This method to validate the email
        /// </summary>
        /// <returns>returns bool value</returns>
        public bool IsEmailFieldValid()
        {
            bool isEmailValid = VEmail.Validate();
            return isEmailValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            VEmail = new ValidatableObject<string>();
        }

        /// <summary>
        /// This method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            VEmail.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email Required" });
            VEmail.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "Invalid Email" });
        }

    }
}
