using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NutritionDiary_app.Services
{
    internal class ApiService
    {
        private RegisterBindingModel model;
        private HttpClient client;

        public async Task<RegisterBindingModel> RegisterUserAsync
            (string name, string email, string password, string confirmPassword)
        {
            client = new HttpClient();
            
            model = new RegisterBindingModel
            {
                ProfileName = name,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await client.PostAsync(
                 Constants.BaseApiAddress + "api/User/Register", httpContent);

            return model;
        }


        public async Task<string> LoginAsync(string email, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password)
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "api/User/Login");

            request.Content = new FormUrlEncodedContent(keyValues);

            client = new HttpClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

            var accessTokenExpiration = jwtDynamic.Value<DateTime>("expires");
            var accessToken = jwtDynamic.Value<string>("jwtToken");
            var userId = jwtDynamic.Value<string>("userId");
            var isValid = jwtDynamic.Value<bool>("isValid");

            Settings.AccessTokenExpirationDate = accessTokenExpiration;
            Settings.UserId = userId;
            Settings.IsValid = isValid;

            Debug.WriteLine(accessTokenExpiration);

            Debug.WriteLine(content);

            return accessToken;
        }

        public async Task<bool> AddProfileAsync
            (string gender, int age, double weight, double height, double activityLevel, double goal, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var profile = new ProfileBindingModel
            {
                Gender = gender,
                Age = age,
                Weight = weight,
                Height = height,
                ActivityLevel = activityLevel,
                Goal = goal
            };

            var json = JsonConvert.SerializeObject(profile);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                    Constants.BaseApiAddress + "api/App/AddProfile", httpContent);


            var content = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

            var result = jwtDynamic.Value<double>("result");
            Settings.Result = result;

            var prots = jwtDynamic.Value<double>("proteins");
            Settings.Proteins = prots;

            var fats = jwtDynamic.Value<double>("fats");
            Settings.Fats = fats;

            var carbs = jwtDynamic.Value<double>("carbs");
            Settings.Carbs = carbs;

            Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            
            return false;
        }


        public async Task<bool> AddFoodInDbAsync
            (string name, double calories, double proteins, double fats, double carbs, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var food = new FoodBindingModel
            {
                FoodName = name,
                Calories = calories,
                Proteins = proteins,
                Fats = fats,
                Carbs = carbs
            };

            var json = JsonConvert.SerializeObject(food);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                 Constants.BaseApiAddress + "api/App/AddFoodInDb", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddMealInDbAsync
            (MealsBindingModel model, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                 Constants.BaseApiAddress + "api/App/AddMealInDb", httpContent);

            var content = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<MealsBindingModel>> GetMealsAsync(string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var response = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/GetMeals");

            var meals = JsonConvert.DeserializeObject<List<MealsBindingModel>>(response);

            return meals;
        }


        public async Task<ProfileBindingModel> GetProfileAsync(string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            
            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/GetProfile");
            var profile = JsonConvert.DeserializeObject<ProfileBindingModel>(json);

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json);

            var metabolism = jwtDynamic.Value<double>("metabolism");
            Settings.Metabolism = metabolism;

            var bmi = jwtDynamic.Value<double>("bmi");
            Settings.BMI = bmi;

            var result = jwtDynamic.Value<double>("result");
            Settings.Result = result;

            var prots = jwtDynamic.Value<double>("proteins");
            Settings.Proteins = prots;

            var fats = jwtDynamic.Value<double>("fats");
            Settings.Fats = fats;

            var carbs = jwtDynamic.Value<double>("carbs");
            Settings.Carbs = carbs;

            var gender = jwtDynamic.Value<string>("gender");
            Settings.Gender = gender;

            var age = jwtDynamic.Value<int>("age");
            Settings.Age = age;

            var weight = jwtDynamic.Value<double>("weight");
            Settings.Weight = weight;

            var height = jwtDynamic.Value<double>("height");
            Settings.Height = height;

            var activity = jwtDynamic.Value<double>("activityLevel");
            Settings.ActivityLevel = activity;

            var goal = jwtDynamic.Value<double>("goal");
            Settings.Goal = goal;

            var id = jwtDynamic.Value<int>("id");
            Settings.ProfileId = id;

            Debug.WriteLine(jwtDynamic);

            return profile;
        }

        public async Task DeleteProfileAsync(string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            Settings.ClearProfile();
            await client.DeleteAsync(
                Constants.BaseApiAddress + "api/App/DeleteProfile");

        }




        public async Task<List<FoodBindingModel>> GetSearchResults(string keyword, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var response = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/SearchFoods/" + keyword);

            var foods = JsonConvert.DeserializeObject<List<FoodBindingModel>>(response);

            return foods;
        }

        //public async Task<List<MealsBindingModel>> GetSearchedMeals(string keyword, string accessToken)
        //{
        //    client = new HttpClient();

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        //        "Bearer", accessToken);

        //    var response = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/SearchMeals/" + keyword);

        //    var meals = JsonConvert.DeserializeObject<List<MealsBindingModel>>(response);

        //    return meals;
        //}




        public async Task<FoodBindingModel> GetFoodAsync(int id, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/GetFood/" + id);
            var food = JsonConvert.DeserializeObject<FoodBindingModel>(json);

            return food;
        }

        public async Task<MealsBindingModel> GetMealAsync(int id, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/GetMeal/" + id);
            var meal = JsonConvert.DeserializeObject<MealsBindingModel>(json);

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json);
            Debug.WriteLine(jwtDynamic);

            return meal;
        }

        public async Task<List<FoodBindingModel>> GetMealFoodsAsync(int id, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/GetMealFoods/" + id);

            var foods = JsonConvert.DeserializeObject<List<FoodBindingModel>>(json);

            JArray jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json);

            foreach (var food in jwtDynamic)
            {
                Debug.WriteLine(food.ToString());
            }

            //Debug.WriteLine(foods);

            return foods;
        }


        public async Task<bool> UpdateMealAsync(int id, MealsBindingModel model, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(
                 Constants.BaseApiAddress + "api/App/UpdateMeal/" + id, httpContent);

            var content = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateProfileAsync(int id, ProfileBindingModel model, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(
                 Constants.BaseApiAddress + "api/App/UpdateProfile/" + id, httpContent);

            var content = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateFoodFromRationAsync(int id, RationBindingModel model, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(
                 Constants.BaseApiAddress + "api/App/UpdateFoodFromRation/" + id, httpContent);

            var content = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> PutFoodInRationAsync(RationBindingModel ration, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(ration);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                 Constants.BaseApiAddress + "api/App/PutFoodInRation", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<RationBindingModel>> GetFoods(string mealTime, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/GetFoods/" + mealTime);

            var foods = JsonConvert.DeserializeObject<List<RationBindingModel>>(response);

            JArray jwtDynamic = JsonConvert.DeserializeObject<dynamic>(response);

            foreach (var food in jwtDynamic)
            {
                Debug.WriteLine(food["id"].ToString());
                Settings.FoodId = (int)food["id"];
            }

            return foods;
        }

        public async Task<List<RationBindingModel>> GetNutrientsQuantity(string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
               "Bearer", accessToken);

            var response = await client.GetStringAsync(Constants.BaseApiAddress + "api/App/GetNutrientsQuantity");
            var foods = JsonConvert.DeserializeObject<List<RationBindingModel>>(response);

            return foods;
        }

        public async Task DeleteFoodFromRationAsync(int id, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            await client.DeleteAsync(
                Constants.BaseApiAddress + "api/App/DeleteFoodFromRation/" + id);

        }

        public async Task DeleteFoodFromMealAsync(int meal_id, int food_id, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            await client.DeleteAsync(
                Constants.BaseApiAddress + "api/App/DeleteFoodFromMeal/" + meal_id + food_id);

        }

        public async Task DeleteFoodFromDbAsync(int id, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            await client.DeleteAsync(
                Constants.BaseApiAddress + "api/App/DeleteFoodFromDb/" + id);

        }

        public async Task DeleteMealFromDbAsync(int id, string accessToken)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            await client.DeleteAsync(
                Constants.BaseApiAddress + "api/App/DeleteMealFromDb/" + id);
        }

    }
}
