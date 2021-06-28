using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.AppLayout.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly ApiService service = new ApiService();

        public ProfilePage()
        {
            InitializeComponent();

            GetProfile();

            if (Settings.ProfileName != null)
            {
                Name_lbl.Text = $"{Settings.ProfileName}";
            }
            Name_lbl.Text = $"{Settings.Email}"; 

            Metabolism_lbl.Text = $"{Settings.Metabolism:0}";
            BMI_lbl.Text = $"{Settings.BMI:0}";
            Result_lbl.Text = $"{Settings.Result:0}";
            Prots_lbl.Text = $"{Settings.Proteins:0}";
            Carbs_lbl.Text = $"{Settings.Carbs:0}";
            Fats_lbl.Text = $"{Settings.Fats:0}";

            if (Settings.Gender == "Male")
            {
                Gender_lbl.Text = "Мужской";
            }
            if (Settings.Gender == "Female")
            {
                Gender_lbl.Text = "Женский";
            }

            Age_lbl.Text = $" {Settings.Age} лет";
            Height_lbl.Text = $" {Settings.Height:0} см";
            Weight_lbl.Text = $" {Settings.Weight:0} кг";

            if (Settings.ActivityLevel == 1.2)
            {
                Activity_lbl.Text = "Cидячий образ жизни";
            }
            if (Settings.ActivityLevel == 1.375)
            {
                Activity_lbl.Text = "Умеренная активность (легкие физические нагрузки либо занятия 1-3 раз в неделю)";
            }
            if (Settings.ActivityLevel == 1.55)
            {
                Activity_lbl.Text = "Средняя активность (занятия 3-5 раз в неделю)";
            }
            if (Settings.ActivityLevel == 1.725)
            {
                Activity_lbl.Text = "Высокая активность (интенсивные нагрузки, занятия 6-7 раз в неделю)";
            }
            if (Settings.ActivityLevel == 1.9)
            {
                Activity_lbl.Text = "Очень высокая активность";
            }


            if (Settings.Goal == 0.8)
            {
                Goal_lbl.Text = "Сбросить вес";
            }
            if (Settings.Goal == 1)
            {
                Goal_lbl.Text = "Удержать вес";
            }
            if (Settings.Goal == 1.2)
            {
                Goal_lbl.Text = "Набрать вес";
            }

        }

        private async void GetProfile()
        {
            try
            {
                await service.GetProfileAsync(Settings.AccessToken);
            }
            catch (NullReferenceException ex) { Debug.WriteLine(ex.Message); }
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            GetProfile();

            var profile = new ProfileBindingModel
            {
                Gender = Settings.Gender, ActivityLevel = Settings.ActivityLevel, Age = Settings.Age,
                Weight = Settings.Weight, Height = Settings.Height, Goal = Settings.Goal
            };
            await Navigation.PushAsync(new EditProfilePage(profile));
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPageTabbedForm());
        }
    }
}
