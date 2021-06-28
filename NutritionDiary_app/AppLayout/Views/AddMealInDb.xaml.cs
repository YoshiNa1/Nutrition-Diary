using System;
using System.Collections.Generic;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using NutritionDiary_app.ViewModels;
using Xamarin.Forms;

namespace NutritionDiary_app.AppLayout.Views
{
    public partial class AddMealInDb : ContentPage
    {
        private readonly ApiService service = new ApiService();

        public List<FoodBindingModel> foundFoods { get; set; }

        public AddMealInDb()
        {
            InitializeComponent();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar.Text != string.Empty)
            {
                searchResults.IsVisible = true;

                foundFoods = await service.GetSearchResults(searchBar.Text, Settings.AccessToken);
                searchResults.ItemsSource = foundFoods;
                searchResults.HeightRequest = foundFoods.Count * 40;
            }
            else
            {
                searchResults.IsVisible = false;
            }
        }

        private void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var viewModel = (AddMealInDbViewModel)BindingContext;

            viewModel.FoodSelectedCommand?.Execute(e);

            searchResults.IsVisible = false;
            searchBar.Text = string.Empty;

            if (viewModel.Foods.Count == 0)
            {
                FoodsList.IsVisible = false;
            }
            FoodsList.IsVisible = true;
            FoodsList.HeightRequest = viewModel.Foods.Count * 55;

        }


        private async void Add_Clicked(object sender, EventArgs e)
        {
            await service.GetMealsAsync(Settings.AccessToken);
            await Navigation.PushAsync(new MealsPage());
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var viewModel = (AddMealInDbViewModel)BindingContext;
            Command RemoveCommand = new Command<FoodBindingModel>((food) => viewModel.Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as FoodBindingModel;
            RemoveCommand.Execute(food);
            FoodsList.HeightRequest = viewModel.Foods.Count * 55;
        }

    }
}
