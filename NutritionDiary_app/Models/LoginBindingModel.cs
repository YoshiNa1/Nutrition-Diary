using System;
using Newtonsoft.Json;

namespace NutritionDiary_app.Models
{
    public class LoginBindingModel
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
