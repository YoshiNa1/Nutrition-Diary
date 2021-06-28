using System;
using Newtonsoft.Json;

namespace NutritionDiary_app.Models
{
    public class RegisterBindingModel
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("ProfileName")]
        public string ProfileName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public ProfileBindingModel profile { get; set; }
        public RationBindingModel ration { get; set; }
    }
}