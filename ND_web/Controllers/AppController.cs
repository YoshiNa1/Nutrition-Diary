using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ND_web.Models;

namespace ND_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppController : ControllerBase
    {
        private ProfileContext pc; private FoodContext fc; private RationContext rc; private MealContext mc;

        public AppController(ProfileContext pcontext, FoodContext fcontext, RationContext rcontext, MealContext mcontext)
        {
            pc = pcontext; fc = fcontext; rc = rcontext; mc = mcontext;
        }



        // POST api/App/AddProfile
        [HttpPost("AddProfile")]
        public async Task<IActionResult> AddProfile([FromBody] ProfileBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.UserId = userId;

                if (model.Gender == "Female")
                {
                    model.Metabolism = (447.593 + (9.247 * model.Weight) + (3.098 * model.Height)
                        - (4.33 * model.Age)) * model.ActivityLevel;

                }
                if (model.Gender == "Male")
                {
                    model.Metabolism = (88.362 + (13.397 * model.Weight) + (4.799 * model.Height)
                        - (5.677 * model.Age)) * model.ActivityLevel;
                }

                model.BMI = model.Weight / Math.Pow(model.Height / 100, 2);
                model.Result = model.Metabolism * model.Goal;

                model.Proteins = (model.Result * 0.3) / 4;
                model.Fats = (model.Result * 0.2) / 4;
                model.Carbs = (model.Result * 0.5) / 4;

                pc.Profiles.Add(model);

                await pc.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return BadRequest();
            }
        }

        // GET api/App/GetProfile/1
        [HttpGet("GetProfile")]
        public ActionResult<ProfileBindingModel> GetProfile()
        {
            var profile = pc.Profiles.FirstOrDefault(i => i.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (profile == null)
            {
                ModelState.AddModelError("", "Profile not found");
                Debug.WriteLine("Profile not found");
                return Ok();
            }

            return profile;
        }

        //PUT api/App/UpdateProfile/1
        [HttpPut("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, ProfileBindingModel profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }
            
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                profile.UserId = userId;

                if (profile.Gender == "Female")
                {
                    profile.Metabolism = (447.593 + (9.247 * profile.Weight) + (3.098 * profile.Height)
                        - (4.33 * profile.Age)) * profile.ActivityLevel;

                }
                if (profile.Gender == "Male")
                {
                    profile.Metabolism = (88.362 + (13.397 * profile.Weight) + (4.799 * profile.Height)
                        - (5.677 * profile.Age)) * profile.ActivityLevel;
                }

                profile.BMI = profile.Weight / Math.Pow(profile.Height / 100, 2);
                profile.Result = profile.Metabolism * profile.Goal;

                profile.Proteins = (profile.Result * 0.3) / 4;
                profile.Fats = (profile.Result * 0.2) / 4;
                profile.Carbs = (profile.Result * 0.5) / 4;

                //pc.Profiles.Add(profile);
                pc.Entry(profile).State = EntityState.Modified;
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return BadRequest();
            }


            try
            {
                await pc.SaveChangesAsync();
                return Ok(profile);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex);
            }

            return NoContent();
        }

        // DELETE api/App/DeleteProfile
        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile()
        {
            var profile = pc.Profiles.FirstOrDefault(i => i.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (profile == null)
            {
                ModelState.AddModelError("", "Profile not found");
                Debug.WriteLine("Profile not found");
                return Ok();
            }

            pc.Profiles.Remove(profile);
            await pc.SaveChangesAsync();

            return NoContent();
        }





        // POST api/App/AddFoodInDb
        [HttpPost("AddFoodInDb")]
        public async Task<IActionResult> AddFoodInDb(FoodBindingModel model)
        {
            if (ModelState.IsValid)
            {
                fc.Foods.Add(model);

                await fc.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return Ok();
            }
        }


        // GET: api/App/SearchFoods/keyword
        [HttpGet("SearchFoods/{keyword}")]
        public IQueryable<FoodBindingModel> SearchFoods(string keyword)
        {
            return fc.Foods
                .Where(f => f.FoodName.Contains(keyword));
        }

        //GET api/App/GetFood/1
        [HttpGet("GetFood/{id}")]
        public async Task<ActionResult<FoodBindingModel>> GetFood(int id)
        {
            var food = await fc.Foods.FindAsync(id);
            if (food == null)
            {
                ModelState.AddModelError("", "Not found");
                return Ok();
            }

            return food;
        }


        // POST api/App/PutFoodInRation
        [HttpPost("PutFoodInRation")]
        public async Task<IActionResult> PutFoodInRation([FromBody] RationBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.UserId = userId;

                model.Time.ToShortTimeString();
                model.Date = DateTime.Today.Date;

                model.CaloriesConsumed = (model.Weight / 100) * model.CaloriesInFood;
                model.ProteinsConsumed = (model.Weight / 100) * model.ProteinsInFood;
                model.FatsConsumed = (model.Weight / 100) * model.FatsInFood;
                model.CarbsConsumed = (model.Weight / 100) * model.CarbsInFood;

                rc.Ration.Add(model);

                await rc.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return Ok();
            }
        }

        // GET api/App/GetFoods/mealTime
        [HttpGet("GetFoods/{mealTime}")]
        public IQueryable<RationBindingModel> GetFoods(string mealTime)
        {
            return rc.Ration
                .Where(r => r.Meal == mealTime
                && r.Date == DateTime.Today.Date
                && r.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        // GET api/App/GetNutrientsQuantity
        [HttpGet("GetNutrientsQuantity")]
        public List<RationBindingModel> GetNutrientsQuantity()
        {
            //double calories = 0;

            var foods = rc.Ration.Where(r => r.Date == DateTime.Today.Date
            && r.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            double calories = foods.Sum(r => r.CaloriesConsumed);
            double proteins = foods.Sum(r => r.ProteinsConsumed);
            double fats = foods.Sum(r => r.FatsConsumed);
            double carbs = foods.Sum(r => r.CarbsConsumed);

            var nutrients = new List<RationBindingModel> { new RationBindingModel {
                CaloriesConsumed = calories,
                ProteinsConsumed = proteins,
                CarbsConsumed = carbs,
                FatsConsumed = fats } };

            return nutrients;
        }





        // POST api/App/AddMealInDb
        [HttpPost("AddMealInDb")]
        public async Task<IActionResult> AddMealInDb(MealsBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.UserId = userId;

                foreach (var food in model.Foods)
                {
                    food.CaloriesConsumed = (food.Weight / 100) * food.Calories;
                    food.ProteinsConsumed = (food.Weight / 100) * food.Proteins;
                    food.FatsConsumed = (food.Weight / 100) * food.Fats;
                    food.CarbsConsumed = (food.Weight / 100) * food.Carbs;
                }

                model.TotalWeight = model.Foods.Sum(f => f.Weight);

                model.CaloriesInMeal = model.Foods.Sum(f => f.CaloriesConsumed) / (model.TotalWeight / 100);
                model.ProteinsInMeal = model.Foods.Sum(f => f.ProteinsConsumed) / (model.TotalWeight / 100);
                model.FatsInMeal = model.Foods.Sum(f => f.FatsConsumed) / (model.TotalWeight / 100);
                model.CarbsInMeal = model.Foods.Sum(f => f.CarbsConsumed) / (model.TotalWeight / 100);

                mc.Meals.Add(model);

                await mc.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return Ok();
            }
        }


        // GET: api/App/SearchMeals/keyword
        [HttpGet("SearchMeals/{keyword}")]
        public IQueryable<MealsBindingModel> SearchMeals(string keyword)
        {
            return mc.Meals
                .Where(f => f.MealName.Contains(keyword));
        }

        //GET api/App/GetMeal/1
        [HttpGet("GetMeal/{id}")]
        public async Task<ActionResult<MealsBindingModel>> GetMeal(int id)
        {
            var meal = await mc.Meals.FindAsync(id);
            if (meal == null)
            {
                ModelState.AddModelError("", "Not found");
                return Ok();
            }
            return meal;
        }


        //GET api/App/GetMeal/1
        [HttpGet("GetMealFoods/{id}")]
        public IEnumerable<FoodBindingModel> GetMealFoods(int id)
        {
            var foods = mc.Foods.Where(f=>f.MealsBindingModelId == id);

            return foods;
        }

        // GET api/App/GetMeals
        [HttpGet("GetMeals")]
        public IQueryable<MealsBindingModel> GetMeals()
        {
            return mc.Meals.Where(f => f.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
        }


        //PUT api/App/UpdateMeal/1
        [HttpPut("UpdateMeal/{id}")]
        public async Task<IActionResult> UpdateMeal(int id, MealsBindingModel meal)
        {
            if (id != meal.Id)
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                meal.UserId = userId;

                foreach (var food in meal.Foods)
                {
                    food.CaloriesConsumed = (food.Weight / 100) * food.Calories;
                    food.ProteinsConsumed = (food.Weight / 100) * food.Proteins;
                    food.FatsConsumed = (food.Weight / 100) * food.Fats;
                    food.CarbsConsumed = (food.Weight / 100) * food.Carbs;
                    mc.Entry(food).State = EntityState.Modified;
                }

                meal.TotalWeight = meal.Foods.Sum(f => f.Weight);

                meal.CaloriesInMeal = meal.Foods.Sum(f => f.CaloriesConsumed) / (meal.TotalWeight / 100);
                meal.ProteinsInMeal = meal.Foods.Sum(f => f.ProteinsConsumed) / (meal.TotalWeight / 100);
                meal.FatsInMeal = meal.Foods.Sum(f => f.FatsConsumed) / (meal.TotalWeight / 100);
                meal.CarbsInMeal = meal.Foods.Sum(f => f.CarbsConsumed) / (meal.TotalWeight / 100);

                mc.Entry(meal).State = EntityState.Modified;
               // mc.Entry(meal.Foods).State = EntityState.Modified;
            }

            else
            {
                ModelState.AddModelError("", "Error");
                return Ok();
            }
            

            try
            {
                await mc.SaveChangesAsync();
                return Ok(meal);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex);
            }

            return NoContent();
        }

        // DELETE api/App/DeleteFoodFromMeal/1
        [HttpDelete("DeleteFoodFromMeal/{meal_id}/{food_id}")]
        public async Task<IActionResult> DeleteFoodFromMeal(int meal_id, int food_id)
        {
            // GetMealFoods(meal_id);

            var food = await mc.Foods.FindAsync(food_id);
            
            if (food == null)
            {
                return NotFound();
            }

            mc.Foods.Remove(food);
            await mc.SaveChangesAsync();

            var foods = GetMealFoods(meal_id);
            var meal = await mc.Meals.FindAsync(meal_id);
            meal.Foods = foods.ToList();
            await UpdateMeal(meal_id, meal);


            return NoContent();
        }



        //PUT api/App/UpdateFoodFromRation/1
        [HttpPut("UpdateFoodFromRation/{id}")]
        public async Task<IActionResult> UpdateFoodFromRation(int id, RationBindingModel food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                food.UserId = userId;

                food.Time.ToShortTimeString();
                food.Date = DateTime.Today.Date;

                food.CaloriesConsumed = (food.Weight / 100) * food.CaloriesInFood;
                food.ProteinsConsumed = (food.Weight / 100) * food.ProteinsInFood;
                food.FatsConsumed = (food.Weight / 100) * food.FatsInFood;
                food.CarbsConsumed = (food.Weight / 100) * food.CarbsInFood;

                rc.Entry(food).State = EntityState.Modified;
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return Ok();
            }

            try
            {
                await rc.SaveChangesAsync();
                return Ok(food);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex);
            }

            return NoContent();
        }

        // DELETE api/App/DeleteFoodFromRation/1
        [HttpDelete("DeleteFoodFromRation/{id}")]
        public async Task<IActionResult> DeleteFoodFromRation(int id)
        {
            var food = await rc.Ration.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            rc.Ration.Remove(food);
            await rc.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/App/DeleteFoodFromDb/1
        [HttpDelete("DeleteFoodFromDb/{id}")]
        public async Task<IActionResult> DeleteFoodFromDb(int id)
        {
            var food = await fc.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            fc.Foods.Remove(food);
            await fc.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/App/DeleteMealFromDb/1
        [HttpDelete("DeleteMealFromDb/{id}")]
        public async Task<IActionResult> DeleteMealFromDb(int id)
        {
            var meal = await mc.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            mc.Meals.Remove(meal);
            await mc.SaveChangesAsync();

            return NoContent();
        }

    }
}
