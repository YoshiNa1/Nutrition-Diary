using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NutritionDiary_app.Models
{
    public class MealsBindingModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("MealName")]
        public string MealName { get; set; }


        [JsonProperty("CaloriesInMeal")]
        public double CaloriesInMeal { get; set; }

        [JsonProperty("ProteinsInMeal")]
        public double ProteinsInMeal { get; set; }

        [JsonProperty("FatsInMeal")]
        public double FatsInMeal { get; set; }

        [JsonProperty("CarbsInMeal")]
        public double CarbsInMeal { get; set; }


        [JsonProperty("TotalWeight")]
        public double TotalWeight { get; set; }

        [JsonProperty("Foods")]
        public List<FoodBindingModel> Foods { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }
    }
}
