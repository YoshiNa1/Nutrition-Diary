using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using NutritionDiary_app.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.AppLayout.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageTabbedForm : ContentPage
    {
       // public List<FoodsList> Meals { get; set; }
        private readonly ApiService service = new ApiService();

        public List<FoodBindingModel> foundFoods { get; set; }

        #region FoodsLists Properties
        public List<RationBindingModel> B_Foods { get; set; }
        public List<RationBindingModel> Sb_Foods { get; set; }
        public List<RationBindingModel> D_Foods { get; set; }
        public List<RationBindingModel> Sd_Foods { get; set; }
        public List<RationBindingModel> E_Foods { get; set; }
        public List<RationBindingModel> Se_Foods { get; set; }
        #endregion

        public List<RationBindingModel> AllFoods { get; set; }

        public string MenuItem { get; set; }
        public List<string> MenuItems { get; set; }

        public MainPageTabbedForm()
        {
            InitializeComponent();

            UpdateCaloriesGauge();

            Load_B_Foods(); Load_Sb_Foods(); Load_D_Foods();
            Load_Sd_Foods(); Load_E_Foods(); Load_Se_Foods();


            UserInfo_lbl.Text = $"AccessToken: {Settings.AccessToken} \nUserId: {Settings.UserId}. \nUser Name: {Settings.ProfileName}. \nCalories: {Settings.Result}." +
                $"\nProteins: {Settings.Proteins}. \nFats: {Settings.Fats}. \nCarbs: {Settings.Carbs}.";


            MenuItems = new List<string>
            {
                "Главная",
                "Профиль",
                "Мои блюда",
                "Выйти"
            };

            MenuPicker.ItemsSource = MenuItems;
            MenuPicker.SelectedItem = MenuItem;

            //LoadFoods();
            //FoodsList b_list, sb_list, d_list, sd_list, e_list, se_list;

            //b_list = new FoodsList("Завтрак");
            //sb_list = new FoodsList("Второй завтрак");
            //d_list = new FoodsList("Обед");
            //sd_list = new FoodsList("Второй обед");
            //e_list = new FoodsList("Ужин");
            //se_list = new FoodsList("Второй ужин");

            //var list = new List<FoodsList>
            //{
            //    b_list, sb_list, d_list, sd_list, e_list, se_list
            //};
            //Meals = list;
            //MealsList.ItemsSource = Meals;
        }


        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFoodInDb());
        }


        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (searchBar.Text != string.Empty)
            {
                searchResults.IsVisible = true;
                addNewFood.IsVisible = true;

                foundFoods = await service.GetSearchResults(searchBar.Text, Settings.AccessToken);
                searchResults.ItemsSource = foundFoods;
                searchResults.HeightRequest = foundFoods.Count * 45;
            }
            else
            {
                searchResults.IsVisible = false;
                addNewFood.IsVisible = false;
            }
        }

        private async void searchResults_ItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var food = e.CurrentSelection.FirstOrDefault() as FoodBindingModel;
            await Navigation.PushAsync(new PutFoodInRation(food));
        }

       

        //private async void Logout_Clicked(object sender, EventArgs e)
        //{
        //    Settings.ClearSettings();
        //    await Navigation.PushAsync(new LoginPage());

        //}

        //private async void Profile_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ProfilePage());
        //}


        private async void MenuPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            MenuItem = MenuPicker.Items[MenuPicker.SelectedIndex];

            if (MenuItem == "Главная")
            {
                await Navigation.PushAsync(new MainPageTabbedForm());
            }
            if (MenuItem == "Профиль")
            {
                await service.GetProfileAsync(Settings.AccessToken);
                await Navigation.PushAsync(new ProfilePage());
            }
            if (MenuItem == "Мои блюда")
            {
                await service.GetMealsAsync(Settings.AccessToken);
                await Navigation.PushAsync(new MealsPage());
            }
            if (MenuItem == "Выйти")
            {
                Settings.ClearSettings();
                await Navigation.PushAsync(new LoginPage());
            }
        }

        #region UpdateGauge Methods
        private void CaloriesButton_Clicked(object sender, EventArgs e)
        {
            UpdateCaloriesGauge();
        }

        private async void UpdateCaloriesGauge()
        {
            AllFoods = await service.GetNutrientsQuantity(Settings.AccessToken);
            CaloriesGauge.ItemsSource = AllFoods;

            CaloriesGauge.IsVisible = true;
            ProteinsGauge.IsVisible = false;
            FatsGauge.IsVisible = false;
            CarbsGauge.IsVisible = false;
        }
        private async void ProteinsButton_Clicked(object sender, EventArgs e)
        {
            AllFoods = await service.GetNutrientsQuantity(Settings.AccessToken);
            ProteinsGauge.ItemsSource = AllFoods;

            ProteinsGauge.IsVisible = true;
            CaloriesGauge.IsVisible = false;
            FatsGauge.IsVisible = false;
            CarbsGauge.IsVisible = false;
        }

        private async void FatsButton_Clicked(object sender, EventArgs e)
        {
            AllFoods = await service.GetNutrientsQuantity(Settings.AccessToken);
            FatsGauge.ItemsSource = AllFoods;

            FatsGauge.IsVisible = true;
            CaloriesGauge.IsVisible = false;
            ProteinsGauge.IsVisible = false;
            CarbsGauge.IsVisible = false;
        }

        private async void CarbsButton_Clicked(object sender, EventArgs e)
        {
            AllFoods = await service.GetNutrientsQuantity(Settings.AccessToken);
            CarbsGauge.ItemsSource = AllFoods;

            CarbsGauge.IsVisible = true;
            CaloriesGauge.IsVisible = false;
            FatsGauge.IsVisible = false;
            ProteinsGauge.IsVisible = false;
        }
        #endregion

        #region LoadFoods Methods
        public async void Load_B_Foods()
        {
            B_Foods = await service.GetFoods("Завтрак", Settings.AccessToken);

            if (B_Foods.Count == 0)
            {
                B_FoodsList.IsVisible= false; 
            }
            else B_FoodsList.ItemsSource = B_Foods;
            B_FoodsList.HeightRequest = B_Foods.Count * 60;
        }

        public async void Load_Sb_Foods()
        {
            Sb_Foods = await service.GetFoods("Второй завтрак", Settings.AccessToken);

            if (Sb_Foods.Count == 0)
            {
                Sb_FoodsList.IsVisible =false;
            }
            else Sb_FoodsList.ItemsSource = Sb_Foods;
            Sb_FoodsList.HeightRequest = Sb_Foods.Count * 60;
        }

        public async void Load_D_Foods()
        {
            D_Foods = await service.GetFoods("Обед", Settings.AccessToken);

            if (D_Foods.Count == 0)
            {
                D_FoodsList.IsVisible = false;
            }
            else D_FoodsList.ItemsSource = D_Foods;
            D_FoodsList.HeightRequest = D_Foods.Count * 60;
        }

        public async void Load_Sd_Foods()
        {
            Sd_Foods = await service.GetFoods("Второй обед", Settings.AccessToken);

            if (Sd_Foods.Count == 0)
            {
                Sd_FoodsList.IsVisible =false;
            }
            else Sd_FoodsList.ItemsSource = Sd_Foods;
            Sd_FoodsList.HeightRequest = Sd_Foods.Count * 60;
        }

        public async void Load_E_Foods()
        {
            E_Foods = await service.GetFoods("Ужин", Settings.AccessToken);

            if (E_Foods.Count == 0)
            {
                E_FoodsList.IsVisible = false;
            }
            else E_FoodsList.ItemsSource = E_Foods;
            E_FoodsList.HeightRequest = E_Foods.Count * 60;
        }

        public async void Load_Se_Foods()
        {
            Se_Foods = await service.GetFoods("Второй ужин", Settings.AccessToken);

            if (Se_Foods.Count == 0)
            {
                Se_FoodsList.IsVisible = false;
            }
            else Se_FoodsList.ItemsSource = Se_Foods;
            Se_FoodsList.HeightRequest = Se_Foods.Count * 60;
        }


        #endregion

        #region DeleteFromRation Methods
        private async void DeleteB_Clicked(object sender, EventArgs e)
        {
            Command RemoveCommand = new Command<RationBindingModel>((food) => B_Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as RationBindingModel;
            RemoveCommand.Execute(food);
            await service.DeleteFoodFromRationAsync(food.Id, Settings.AccessToken);
            B_FoodsList.ItemsSource = null;
            Load_B_Foods();
        }
        private async void DeleteSb_Clicked(object sender, EventArgs e)
        {
            Command RemoveCommand = new Command<RationBindingModel>((food) => Sb_Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as RationBindingModel;
            RemoveCommand.Execute(food);
            await service.DeleteFoodFromRationAsync(food.Id, Settings.AccessToken);
            Sb_FoodsList.ItemsSource = null;
            Load_Sb_Foods();
        }
        private async void DeleteD_Clicked(object sender, EventArgs e)
        {
            Command RemoveCommand = new Command<RationBindingModel>((food) => D_Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as RationBindingModel;
            RemoveCommand.Execute(food);
            await service.DeleteFoodFromRationAsync(food.Id, Settings.AccessToken);
            D_FoodsList.ItemsSource = null;
            Load_D_Foods();
        }
        private async void DeleteSd_Clicked(object sender, EventArgs e)
        {
            Command RemoveCommand = new Command<RationBindingModel>((food) => Sd_Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as RationBindingModel;
            RemoveCommand.Execute(food);
            await service.DeleteFoodFromRationAsync(food.Id, Settings.AccessToken);
            Sd_FoodsList.ItemsSource = null;
            Load_Sd_Foods();
        }
        private async void DeleteE_Clicked(object sender, EventArgs e)
        {
            Command RemoveCommand = new Command<RationBindingModel>((food) => E_Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as RationBindingModel;
            RemoveCommand.Execute(food);
            await service.DeleteFoodFromRationAsync(food.Id, Settings.AccessToken);
            E_FoodsList.ItemsSource = null;
            Load_E_Foods();
        }
        private async void DeleteSe_Clicked(object sender, EventArgs e)
        {
            Command RemoveCommand = new Command<RationBindingModel>((food) => Se_Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as RationBindingModel;
            RemoveCommand.Execute(food);
            await service.DeleteFoodFromRationAsync(food.Id, Settings.AccessToken);
            Se_FoodsList.ItemsSource = null;
            Load_Se_Foods();
        }
        #endregion

        #region EditFood Methods
        private async void EditB_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var _food = button.BindingContext as RationBindingModel;
            var food = new FoodBindingModel
            {
                FoodName = _food.FoodName,
                Calories = _food.CaloriesInFood,
                Proteins = _food.ProteinsInFood,
                Carbs = _food.CarbsInFood,
                Fats = _food.FatsInFood
            };

            await Navigation.PushAsync(new UpdateFoodFromRation(food, _food));
        }

        private async void EditSb_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var _food = button.BindingContext as RationBindingModel;
            var food = new FoodBindingModel
            {
                FoodName = _food.FoodName,
                Calories = _food.CaloriesInFood,
                Proteins = _food.ProteinsInFood,
                Carbs = _food.CarbsInFood,
                Fats = _food.FatsInFood
            };

            await Navigation.PushAsync(new UpdateFoodFromRation(food, _food));
        }

        private async void EditD_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var _food = button.BindingContext as RationBindingModel;
            var food = new FoodBindingModel
            {
                FoodName = _food.FoodName,
                Calories = _food.CaloriesInFood,
                Proteins = _food.ProteinsInFood,
                Carbs = _food.CarbsInFood,
                Fats = _food.FatsInFood
            };

            await Navigation.PushAsync(new UpdateFoodFromRation(food, _food));
        }
        private async void EditSd_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var _food = button.BindingContext as RationBindingModel;
            var food = new FoodBindingModel
            {
                FoodName = _food.FoodName,
                Calories = _food.CaloriesInFood,
                Proteins = _food.ProteinsInFood,
                Carbs = _food.CarbsInFood,
                Fats = _food.FatsInFood
            };

            await Navigation.PushAsync(new UpdateFoodFromRation(food, _food));
        }
        private async void EditE_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var _food = button.BindingContext as RationBindingModel;
            var food = new FoodBindingModel
            {
                FoodName = _food.FoodName,
                Calories = _food.CaloriesInFood,
                Proteins = _food.ProteinsInFood,
                Carbs = _food.CarbsInFood,
                Fats = _food.FatsInFood
            };

            await Navigation.PushAsync(new UpdateFoodFromRation(food, _food));
        }
        private async void EditSe_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var _food = button.BindingContext as RationBindingModel;
            var food = new FoodBindingModel
            {
                FoodName = _food.FoodName,
                Calories = _food.CaloriesInFood,
                Proteins = _food.ProteinsInFood,
                Carbs = _food.CarbsInFood,
                Fats = _food.FatsInFood
            };

            await Navigation.PushAsync(new UpdateFoodFromRation(food, _food));
        }

        #endregion

        //public class FoodsList : List<RationBindingModel>
        //{
        //    ApiService service = new ApiService();

        //    public string MealName { get; set; }

        //    //child List
        //    public List<RationBindingModel> Foods { get; set; }

        //    public FoodsList(string name)
        //    {

        //        MealName = name;
        //        GetFoodsAsync(name);
        //    }

        //    public async void GetFoodsAsync(string name)
        //    {
        //        Foods = await service.GetFoods(name, Settings.AccessToken);
        //    }
        //}

        //private async void SessionButton_Clicked(object sender, EventArgs e)
        //{
        //    AllFoods = await service.GetNutrientsQuantity();
        //    All_FoodsList.ItemsSource = AllFoods;

        //    All_FoodsList.IsVisible = true;
        //    just_lbl.IsVisible = false;
        //}
    }
}
