using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Xamarin.Forms;

namespace NutritionDiary_app.AppLayout.Views
{
    public partial class EditProfilePage : ContentPage
    {
        private readonly ApiService service = new ApiService();
        private ProfileBindingModel _profile;

        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        public string Goal { get; set; }
        public double GoalCoefficient { get; set; }

        public string ActivityLevel { get; set; }
        public double ActivityCoefficient { get; set; }

        public List<string> Activities { get; set; }
        public List<string> Goals { get; set; }

        public EditProfilePage(ProfileBindingModel profile)
        {
            InitializeComponent();

            _profile = profile;

            profile.Id = Settings.ProfileId;

            Activities = new List<string>
            {
                "Cидячий образ жизни",
                "Умеренная активность (легкие физические нагрузки либо занятия 1-3 раз в неделю)",
                "Средняя активность (занятия 3-5 раз в неделю)",
                "Высокая активность (интенсивные нагрузки, занятия 6-7 раз в неделю)",
                "Очень высокая активность"
            };

            ActivitiesPicker.ItemsSource = Activities;
            ActivitiesPicker.SelectedItem = ActivityLevel;


            AgeEntry.Placeholder = profile.Age.ToString();
            AgeEntry.Text = AgeEntry.Placeholder;

            HeightEntry.Placeholder = profile.Height.ToString();
            HeightEntry.Text = HeightEntry.Placeholder;

            WeightEntry.Placeholder = profile.Weight.ToString();
            WeightEntry.Text = WeightEntry.Placeholder;

            Debug.WriteLine($"!!!!!!!!!!!!\n{profile.Id}\n!!!!!!!!!!!!");

        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            Age = int.Parse(AgeEntry.Text);
            Weight = double.Parse(WeightEntry.Text);
            Height = double.Parse(HeightEntry.Text);

            var goal_check = GoalGroup.CheckedItem;
            if (goal_check == firstGoal)
            {
                GoalCoefficient = 0.8;
            }
            if (goal_check == secondGoal)
            {
                GoalCoefficient = 1;
            }
            if (goal_check == thirdGoal)
            {
                GoalCoefficient = 1.2;
            }

            var activity_index = ActivitiesPicker.SelectedIndex;
            if (activity_index == 0)
            {
                ActivityCoefficient = 1.2;
            }
            if (activity_index == 1)
            {
                ActivityCoefficient = 1.375;
            }
            if (activity_index == 2)
            {
                ActivityCoefficient = 1.55;
            }
            if (activity_index == 3)
            {
                ActivityCoefficient = 1.725;
            }
            if (activity_index == 4)
            {
                ActivityCoefficient = 1.9;
            }

            var model = new ProfileBindingModel
            {
                Gender = Settings.Gender,
                Age = Age,
                Weight = Weight,
                Height = Height,
                ActivityLevel = ActivityCoefficient,
                Goal = GoalCoefficient,
                Id = _profile.Id
            };

            await service.UpdateProfileAsync(_profile.Id, model, Settings.AccessToken);

            Debug.WriteLine($"!!!!!!!!!!!!\n{_profile.Id}\n!!!!!!!!!!!!");
            //await service.AddProfileAsync
            //    (Settings.Gender, Age, Weight, Height, ActivityCoefficient, GoalCoefficient, Settings.AccessToken);

            //await service.DeleteProfileAsync(Settings.AccessToken);

            await service.GetProfileAsync(Settings.AccessToken);
            await Navigation.PushAsync(new ProfilePage());
        }

        private void ActivitiesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivityLevel = ActivitiesPicker.Items[ActivitiesPicker.SelectedIndex];
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
