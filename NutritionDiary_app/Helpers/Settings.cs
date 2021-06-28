using NutritionDiary_app.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;

namespace NutritionDiary_app.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;


        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault("Email", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Email", value);
            }
        }

        public static string ProfileName
        {
            get
            {
                return AppSettings.GetValueOrDefault("ProfileName", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("ProfileName", value);
            }
        }


        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault("UserId", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("UserId", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
            }
        }

        public static bool IsValid
        {
            get
            {
                return AppSettings.GetValueOrDefault("IsValid", false);
            }
            set
            {
                AppSettings.AddOrUpdateValue("IsValid", value);
            }
        }

        public static double Result
        {
            get
            {
                return AppSettings.GetValueOrDefault("Result", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Result", value);
            }
        }


        public static double Proteins
        {
            get
            {
                return AppSettings.GetValueOrDefault("Proteins", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Proteins", value);
            }
        }


        public static double Fats
        {
            get
            {
                return AppSettings.GetValueOrDefault("Fats", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Fats", value);
            }
        }

        public static double Carbs
        {
            get
            {
                return AppSettings.GetValueOrDefault("Carbs", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Carbs", value);
            }
        }

        public static double BMI
        {
            get
            {
                return AppSettings.GetValueOrDefault("BMI", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("BMI", value);
            }
        }


        public static double Metabolism
        {
            get
            {
                return AppSettings.GetValueOrDefault("Metabolism", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Metabolism", value);
            }
        }

        public static int ProfileId
        {
            get
            {
                return AppSettings.GetValueOrDefault("ProfileId", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("ProfileId", value);
            }
        }

        public static string Gender
        {
            get
            {
                return AppSettings.GetValueOrDefault("Gender", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Gender", value);
            }
        }

        public static int Age
        {
            get
            {
                return AppSettings.GetValueOrDefault("Age", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Age", value);
            }
        }

        public static double Weight
        {
            get
            {
                return AppSettings.GetValueOrDefault("Weight", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Weight", value);
            }
        }

        public static double Height
        {
            get
            {
                return AppSettings.GetValueOrDefault("Height", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Height", value);
            }
        }

        public static double ActivityLevel
        {
            get
            {
                return AppSettings.GetValueOrDefault("ActivityLevel", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("ActivityLevel", value);
            }
        }

        public static double Goal
        {
            get
            {
                return AppSettings.GetValueOrDefault("Goal", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Goal", value);
            }
        }
        
        public static int FoodId
        {
            get
            {
                return AppSettings.GetValueOrDefault("FoodId", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("FoodId", value);
            }
        }

        public static void ClearSettings() => AppSettings.Clear();
        public static void ClearProfile()
        {
            Age = 0;
            Height = 0;
            Weight = 0;
            Goal = 0;
            ActivityLevel = 0;
            Metabolism = 0;
            BMI = 0;
            Result = 0;
            Proteins = 0;
            Fats = 0;
            Carbs = 0;
        }
    }
}
