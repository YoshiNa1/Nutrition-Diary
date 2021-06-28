using System;
using Newtonsoft.Json;
using NutritionDiary_app.Helpers;

namespace NutritionDiary_app.Models
{
    public class RationBindingModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }


        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("FoodName")]
        public string FoodName { get; set; }

        [JsonProperty("Weight")]
        public double Weight { get; set; }

        [JsonProperty("Time")]
        public DateTime Time { get; set; }

        [JsonProperty("Meal")]
        public string Meal { get; set; }



        [JsonProperty("CaloriesInFood")]
        public double CaloriesInFood { get; set; }

        [JsonProperty("ProteinsInFood")]
        public double ProteinsInFood { get; set; }

        [JsonProperty("FatsInFood")]
        public double FatsInFood { get; set; }

        [JsonProperty("CarbsInFood")]
        public double CarbsInFood { get; set; }



        [JsonProperty("CaloriesConsumed")]
        public double CaloriesConsumed { get; set; }

        [JsonProperty("ProteinsConsumed")]
        public double ProteinsConsumed { get; set; }

        [JsonProperty("FatsConsumed")]
        public double FatsConsumed { get; set; }

        [JsonProperty("CarbsConsumed")]
        public double CarbsConsumed { get; set; }


        [JsonProperty("UserId")]
        public string UserId { get; set; }


        public double Result => Settings.Result;
        public double Proteins => Settings.Proteins;
        public double Fats => Settings.Fats;
        public double Carbs => Settings.Carbs;

        //public override string ToString()
        //{
        //    // Format your product name here as you want it to be displayed.
        //    return $"{FoodName}, {CaloriesConsumed} kcal, {Time:HH:mm}, {Weight} г";
        //}

    }
}
