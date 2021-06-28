using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionDiary_app.AppLayout.Views;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.Views.Forms
{
    /// <summary>
    /// This page used for adding Profile.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProfilePage : ContentPage
    {
        private readonly ApiService service = new ApiService();

        public string Gender { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        public string Goal { get; set; }
        public double GoalCoefficient { get; set; }

        public string ActivityLevel { get; set; }
        public double ActivityCoefficient { get; set; }

        public List<string> Activities { get; set; }
        public List<string> Goals { get; set; }

        public AddProfilePage()
        {
            InitializeComponent();

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

            Token_lbl.Text = $"Token {Settings.AccessToken} of user {Settings.UserId} Calories: {Settings.Result}";
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            Age = int.Parse(AgeEntry.Text);
            Weight = double.Parse(WeightEntry.Text);
            Height = double.Parse(HeightEntry.Text);

            var gender_check = GenderGroup.CheckedItem;
            if (gender_check == female)
            {
                Gender = "Female";
            }
            if (gender_check == male)
            {
                Gender = "Male";
            }

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
            //profile = new ProfileBindingModel
            //{
            //    Gender = Gender,
            //    Age = Age,
            //    Weight = Weight,
            //    Height = Height,
            //    ActivityLevel = Coefficient,
            //    Goal = Goal
            //};

            await service.AddProfileAsync
                (Gender, Age, Weight, Height, ActivityCoefficient, GoalCoefficient, Settings.AccessToken);

            await Navigation.PushAsync(new MainPageTabbedForm());

            if (Settings.Result == 0)
            {
                await Navigation.PushAsync(new AddProfilePage());
            }
        }

        private void ActivitiesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivityLevel = ActivitiesPicker.Items[ActivitiesPicker.SelectedIndex];
        }
    }
}
