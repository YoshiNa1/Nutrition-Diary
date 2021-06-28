using System.Collections.Generic;
using System.Collections.ObjectModel;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Syncfusion.SfGauge.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NutritionDiary_app.ViewModels
{

    [Preserve(AllMembers = true)]
    public class OverviewViewModel : BaseViewModel
    {
        private ObservableCollection<Nutrient> selectedCalorieItems;

        private ObservableCollection<Pointer> pointers;

        private double scaleEndValue;

        private double cons_quantity;

        private double quantity;

        private string consumed_lbl;

        public OverviewViewModel()
        {
           // Load_Foods();

            Pointers = new ObservableCollection<Pointer>();

            CardItems = new List<NutrientsCard>()
            {
                new NutrientsCard()
                {
                    Icon = "\ue750",
                    Session = "Calories",
                    //EnableButton = true,
                },
                new NutrientsCard()
                {
                    Icon = "\ue74e",
                    Session = "Proteins",
                },
                new NutrientsCard()
                {
                    Icon = "\ue74f",
                    Session = "Fats",
                },
                new NutrientsCard()
                {
                    Icon = "\ue74b",
                    Session = "Carbs",
                },
            };

            var food = new RationBindingModel
            {
                CaloriesConsumed = 167,
                CarbsConsumed = 1.3,
                ProteinsConsumed = 12,
                FatsConsumed = 13
            };

            CaloriesQuantity = food.CaloriesConsumed;

            ProteinsQuantity = food.ProteinsConsumed;

            FatsQuantity = food.CarbsConsumed;

            CarbsQuantity = food.FatsConsumed;

            Calories = new ObservableCollection<Nutrient>()
            {
                new Nutrient()
                {
                    Quantity = CaloriesQuantity, Name = "Calories", Indicator = "#7cf74c",
                }
            };

            Proteins = new ObservableCollection<Nutrient>()
            {
                new Nutrient()
                {
                    Quantity = ProteinsQuantity, Name = "Proteins", Indicator = "#5588fe"
                }
            };

            Fats = new ObservableCollection<Nutrient>()
            {
                new Nutrient()
                {
                    Quantity = FatsQuantity, Name = "Fat", Indicator = "#fe6751",
                }
            };

            Carbs = new ObservableCollection<Nutrient>()
            {
                new Nutrient()
                {
                    Quantity = CarbsQuantity, Name = "Carbs", Indicator = "#fd50c8",
                }
            };

            SelectedSessionCaloriesCard = CardItems[0];

            SelectedCalorieItems = Calories;

            SessionCommand = new Command(SessionButtonClicked);


            //UpdateGauge();
        }

        public Command SessionCommand { get; set; }

        public List<NutrientsCard> CardItems { get; private set; }

        public ObservableCollection<Nutrient> Calories { get; private set; }
        public ObservableCollection<Nutrient> Proteins { get; private set; }
        public ObservableCollection<Nutrient> Fats { get; private set; }
        public ObservableCollection<Nutrient> Carbs { get; private set; }


        public double CaloriesQuantity { get; set; }
        public double FatsQuantity { get; set; }
        public double ProteinsQuantity { get; set; }
        public double CarbsQuantity { get; set; }


        public List<RationBindingModel> Foods { get; set; }

        private readonly ApiService service = new ApiService();

        public ObservableCollection<Nutrient> SelectedCalorieItems
        {
            get
            {
                return selectedCalorieItems;
            }

            private set
            {
                SetProperty(ref selectedCalorieItems, value);
            }
        }

        public double ConsumedQuantity
        {
            get { return cons_quantity; }

            set { SetProperty(ref cons_quantity, value); }
        }

        public double Quantity
        {
            get { return quantity; }

            set { SetProperty(ref quantity, value); }
        }

        public string ConsumedLabel
        {
            get { return consumed_lbl; }

            set { SetProperty(ref consumed_lbl, value); }
        }

        public NutrientsCard SelectedSessionCaloriesCard { get; set; }

        public ObservableCollection<Pointer> Pointers
        {
            get { return pointers; }

            private set { SetProperty(ref pointers, value); }
        }

        public double ScaleEndValue
        {
            get { return scaleEndValue; }

            set { SetProperty(ref scaleEndValue, value); }
        }

        private async void SessionButtonClicked(object obj)
        {
            Foods = await service.GetNutrientsQuantity(Settings.AccessToken);

            
            var food = new RationBindingModel
            {
                CaloriesConsumed = 167, CarbsConsumed = 1.3, ProteinsConsumed = 12, FatsConsumed = 13
            };

            Foods = new List<RationBindingModel> { food };

            CaloriesQuantity = food.CaloriesConsumed;

            ProteinsQuantity = food.ProteinsConsumed;

            FatsQuantity = food.FatsConsumed;

            CarbsQuantity = food.CarbsConsumed;

            SelectedSessionCaloriesCard.EnableButton = false;

            var context = obj as NutrientsCard;
            context.EnableButton = true;
            SelectedSessionCaloriesCard = context;

            switch (SelectedSessionCaloriesCard.Session)
            {
                case "Calories":
                    {
                        SelectedCalorieItems = Calories;
                        ConsumedQuantity = CaloriesQuantity;
                        ConsumedLabel = "Calories Consumed";
                        Quantity = 1500;
                        UpdateGauge(Quantity);
                        break;
                    }

                case "Proteins":
                    {
                        SelectedCalorieItems = Proteins;
                        ConsumedQuantity = ProteinsQuantity;
                        ConsumedLabel = "Proteins Consumed";
                        Quantity = 200;
                        UpdateGauge(Quantity);
                        break;
                    }

                case "Fats":
                    {
                        SelectedCalorieItems = Fats;
                        ConsumedQuantity = FatsQuantity;
                        ConsumedLabel = "Fats Consumed";
                        Quantity = 120;
                        UpdateGauge(Quantity);
                        break;
                    }

                case "Carbs":
                    {
                        SelectedCalorieItems = Carbs;
                        ConsumedQuantity = CarbsQuantity;
                        ConsumedLabel = "Carbs Consumed";
                        Quantity = 300;
                        UpdateGauge(Quantity);
                        break;
                    }
            }
        }


        public async void Load_Foods()
        {
            Foods = await service.GetNutrientsQuantity(Settings.AccessToken);
        }


        //private void UpdateGauge()
        //{
        //    Pointers.Clear();
        //    var ranges = new ObservableCollection<Pointer>();
        //    double rangeStart = 0;

        //    // var items = selectedCalorieItems;
        //    var caloriesRange = new RangePointer();

        //    for (int i = 0; i < SelectedCalorieItems.Count; i++)
        //    {
        //        RangePointer range = new RangePointer()
        //        {
        //            RangeStart = rangeStart,
        //            Value = rangeStart + SelectedCalorieItems[i].Quantity,
        //            Offset = 0.9,
        //            Thickness = 12,
        //            EnableAnimation = false,
        //            Color = Color.FromHex(SelectedCalorieItems[i].Indicator),
        //        };

        //        if (SelectedCalorieItems[i].Nutrient == "Calories")
        //        {
        //            range.Offset = 0.93;
        //            range.Thickness = 18;
        //            range.RangeStart -= 3;
        //            range.Value += 3;
        //            range.RangeCap = RangeCap.Both;
        //            caloriesRange = range;
        //        }

        //        else
        //        {
        //            ranges.Add(range);
        //        }

        //        rangeStart += SelectedCalorieItems[i].Quantity;
        //    }


        //    ScaleEndValue = rangeStart;
        //    Pointers = ranges;
        //    Pointers.Add(caloriesRange);
        //}

        private void UpdateGauge(double endvalue)
        {
            Pointers.Clear();
            var ranges = new ObservableCollection<Pointer>();
            double rangeStart = 0;

            for (int i = 0; i < SelectedCalorieItems.Count; i++)
            {
                RangePointer range = new RangePointer()
                {
                    RangeStart = rangeStart,
                    Value = rangeStart + SelectedCalorieItems[i].Quantity,
                    Offset = 0.9,
                    Thickness = 12,
                    EnableAnimation = false,
                    Color = Color.FromHex(SelectedCalorieItems[i].Indicator),
                };

                ranges.Add(range);
                rangeStart += SelectedCalorieItems[i].Quantity;
            }

            ScaleEndValue = endvalue;
            Pointers = ranges;
        }





        //        public CaloriesCard SelectedSessionCaloriesCard { get; set; }

        //        public OverviewViewModel()
        //        {
        //            CardItems = new List<CaloriesCard>()
        //            {
        //                new CaloriesCard()
        //                {
        //                    Icon = "",
        //                    Session = "Всего калорий",
        //                    EnableButton = true
        //                },
        //                new CaloriesCard()
        //                {
        //                    Icon = "",
        //                    Session = "Белки",
        //                },
        //                new CaloriesCard()
        //                {
        //                    Icon = "",
        //                    Session = "Жиры"
        //                },
        //                new CaloriesCard()
        //                {
        //                    Icon = "",
        //                    Session = "Углеводы"
        //                }
        //            };

        //            CaloriesQuantity = BreakfastCalories.Quantity + DinnerCalories.Quantity + EveningCalories.Quantity;
        //            ProteinsQuantity = BreakfastProteins.Quantity + DinnerProteins.Quantity + EveningProteins.Quantity;
        //            FatsQuantity = BreakfastFats.Quantity + DinnerFats.Quantity + EveningFats.Quantity;
        //            CarbsQuantity = BreakfastCarbs.Quantity + DinnerCarbs.Quantity + EveningCarbs.Quantity;


        //            Calories = new ObservableCollection<Calorie>()
        //            {
        //                new Calorie(CaloriesQuantity, "Всего калорий", "#7cf74c"),
        //                new Calorie(ProteinsQuantity, "Белков", "#5588fe"),
        //                new Calorie(FatsQuantity, "Жиров", "#a83832"),
        //                new Calorie(CarbsQuantity, "Углеводов", "#fd50c8")
        //            };

        //            Proteins = new ObservableCollection<Calorie>()
        //            {
        //                new Calorie(ProteinsQuantity, "Белки", "#5588fe")
        //            };

        //            Fats = new ObservableCollection<Calorie>()
        //            {
        //                new Calorie(FatsQuantity, "Жиры", "#a83832")
        //            };

        //            Carbs = new ObservableCollection<Calorie>()
        //            {
        //                new Calorie(CarbsQuantity, "Углеводы", "#fd50c8")
        //            };


        //            SelectedSessionCaloriesCard = CardItems[0];

        //            SessionCommand = new Command(SessionButtonClicked);

        ////            profile.Result = Result;
        //        }

        //        public Command SessionCommand { get; set; }

        //        public List<CaloriesCard> CardItems { get; set; }

        //        public ObservableCollection<Calorie> Calories { get; set; }
        //        public ObservableCollection<Calorie> Proteins { get; set; }
        //        public ObservableCollection<Calorie> Fats { get; set; }
        //        public ObservableCollection<Calorie> Carbs { get; set; }

        //        public double CaloriesQuantity { get; set; }
        //        public double FatsQuantity { get; set; }
        //        public double ProteinsQuantity { get; set; }
        //        public double CarbsQuantity { get; set; }

        //        public Calorie BreakfastCalories = new Calorie(392, "Всего калорий", "#7cf74c");
        //        public Calorie BreakfastFats = new Calorie(19, "Жиры", "#a83832");
        //        public Calorie BreakfastProteins = new Calorie(36, "Белки", "#5588fe");
        //        public Calorie BreakfastCarbs = new Calorie(100, "Углеводы", "#fd50c8");

        //        public Calorie DinnerCalories = new Calorie(601, "Всего калорий", "#7cf74c");
        //        public Calorie DinnerFats = new Calorie(23, "Жиры", "#a83832");
        //        public Calorie DinnerProteins = new Calorie(50, "Белки", "#5588fe");
        //        public Calorie DinnerCarbs = new Calorie(80, "Углеводы", "#fd50c8");

        //        public Calorie EveningCalories = new Calorie(340, "Всего калорий", "#7cf74c");
        //        public Calorie EveningFats = new Calorie(10, "Жиры", "#a83832");
        //        public Calorie EveningProteins = new Calorie(30, "Белки", "#5588fe");
        //        public Calorie EveningCarbs = new Calorie(23, "Углеводы", "#fd50c8");


        //        public double Result { get; set; }

        //        private void SessionButtonClicked(object obj)
        //        {
        //            // Do something
        //        }

        //    }
    }
}

