using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Xamarin.Forms;

namespace NutritionDiary_app.AppLayout.Views
{
    public partial class MealsPage : ContentPage
    {
        private readonly ApiService service = new ApiService();

        public List<MealsBindingModel> Meals { get; set; }

        public MealsPage()
        {
            InitializeComponent();

            GetMeals();

        }

        private async void MealsList_ItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var meal = e.CurrentSelection.FirstOrDefault() as MealsBindingModel;
            await Navigation.PushAsync(new PutMealInRation(meal));
        }
       

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMealInDb());
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPageTabbedForm());
        }

        private async void GetMeals()
        {
            Meals = await service.GetMealsAsync(Settings.AccessToken);
            MealsList.ItemsSource = Meals;

            if (Meals.Count == 0)
            {
                MealsList.IsVisible = false;
            }
            else MealsList.IsVisible = true;
            MealsList.HeightRequest = Meals.Count * 55;
        }
    }
}
