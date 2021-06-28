using System.Collections.ObjectModel;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using NutritionDiary_app.Validators;
using NutritionDiary_app.Validators.Rules;
using NutritionDiary_app.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NutritionDiary_app.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        private readonly ApiService service = new ApiService();

        private string profilename;
        public string ProfileName
        {
            get { return profilename; }
            set
            {
                if (profilename != value)
                {
                    profilename = value;
                    NotifyPropertyChanged("ProfileName");
                }
            }
        }

        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                if (confirmpassword != value)
                {
                    confirmpassword = value;
                    NotifyPropertyChanged("ConfirmPassword");
                }
            }
        }

        public SignUpPageViewModel()
        {
            Email = Settings.Email;
            Password = Settings.Password;
            InitializeProperties();
            AddValidationRules();
        }

        public Command RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await service.RegisterUserAsync
                        (ProfileName, Email, Password, ConfirmPassword);

                    Settings.Email = Email;
                    Settings.Password = Password;
                    Settings.ProfileName = ProfileName;
                });
            }
        }



        #region Property

        private ValidatableObject<string> name;
        private ValidatablePair<string> password;

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public ValidatableObject<string> VName
        {
            get
            {
                return name;
            }

            set
            {
                if (name == value)
                {
                    return;
                }

                SetProperty(ref name, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public ValidatablePair<string> VPassword
        {
            get
            {
                return password;
            }

            set
            {
                if (password == value)
                {
                    return;
                }

                SetProperty(ref password, value);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize whether fieldsvalue are true or false.
        /// </summary>
        /// <returns>true or false </returns>
        public bool AreFieldsValid()
        {
            bool isEmail = VEmail.Validate();
            bool isNameValid = VName.Validate();
            bool isPasswordValid = VPassword.Validate();
            return isPasswordValid && isNameValid && isEmail;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            VName = new ValidatableObject<string>();
            VPassword = new ValidatablePair<string>();
        }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            VName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name Required" });
            VPassword.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
            VPassword.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Re-enter Password" });
        }



        #endregion

    }
}