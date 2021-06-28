using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace NutritionDiary_app.Models
{
    public class FoodBindingModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }


        [JsonProperty("FoodName")]
        public string FoodName { get; set; }


        [JsonProperty("Calories")]
        public double Calories { get; set; }

        [JsonProperty("Proteins")]
        public double Proteins { get; set; }

        [JsonProperty("Fats")]
        public double Fats { get; set; }

        [JsonProperty("Carbs")]
        public double Carbs { get; set; }


        [JsonProperty("CaloriesConsumed")]
        public double CaloriesConsumed { get; set; }

        [JsonProperty("ProteinsConsumed")]
        public double ProteinsConsumed { get; set; }

        [JsonProperty("FatsConsumed")]
        public double FatsConsumed { get; set; }

        [JsonProperty("CarbsConsumed")]
        public double CarbsConsumed { get; set; }


        [JsonProperty("Weight")]
        public double Weight { get; set; }

        [JsonProperty("MealsBindingModelId")]
        public int MealsBindingModelId { get; set; }
    }
}
