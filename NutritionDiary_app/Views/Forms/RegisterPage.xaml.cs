using System;
using System.Threading.Tasks;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using NutritionDiary_app.ViewModels.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class RegisterPage : ContentPage
    {

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            //var vm = (SignUpPageViewModel)BindingContext;

            //await DisplayAlert("", vm.ProfileName, "OK");

            //if (Settings.Result == null)
            //{
            //    await Navigation.PushAsync(new AddProfilePage());
            //}
            await Navigation.PushAsync(new LoginPage());
        }
    }
}

