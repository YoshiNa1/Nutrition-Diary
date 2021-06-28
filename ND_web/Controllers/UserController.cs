using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ND_web.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ND_web.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ND_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext db; 
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        

        public UserController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            ILogger<UserController> logger, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context; 
            _logger = logger;
            _userService = userService;
        }

        // POST api/User/Register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                //model.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.ProfileName,
                    Email = model.Email,
                 //   Id = model.Id
                };

                // добавляем пользователя
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);


                var foundEmail = await _userManager.FindByEmailAsync(model.Email);

                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, true);
                    return Ok();
                }

                else if (foundEmail != null)
                {
                    ModelState.AddModelError("", $"Email {model.Email} уже используется!");
                }

                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return Ok(model);
        }


        //// POST api/User/Login
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromForm]LoginBindingModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var email = await _userManager.FindByEmailAsync(model.Email);
        //        if (email != null)
        //        {
        //            await _signInManager.SignOutAsync();
        //            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(email, model.Password, true, false);

        //            if (result.Succeeded)
        //                return Ok();
        //        }
        //        ModelState.AddModelError(nameof(model.Email), "Invalid Email or password");
        //    }

        //    return Ok(model);

        //}



        // POST api/User/Login
        [HttpPost("Login"), Consumes("application/x-www-form-urlencoded")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromForm]LoginBindingModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtToken = new JwtSecurityToken(
                    AuthOptions.ISSUER,
                    AuthOptions.AUDIENCE,
                    claims,
                    expires: DateTime.UtcNow.AddMonths(12),
                    signingCredentials: credentials);
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                var expires = new JwtSecurityTokenHandler().ReadToken(token).ValidTo;

                _logger.LogInformation($"User [{model.Email}] logged in the system.");

                if (!ModelState.IsValid) { return BadRequest("Invalid request"); }
                if (!_userService.IsValidUser(model.Email, model.Password)) { return BadRequest("Invalid request"); }

                return Ok(new LoginResult
                {
                    Email = model.Email,
                    JwtToken = token,
                    Expires = expires,
                    UserId = user.Id,
                    IsValid = true
                });
            }
            return Ok(new LoginResult { IsValid = false });
        }



        


        // POST api/User/Logout
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            //Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            await _signInManager.SignOutAsync();
            return Ok();
        }



            //    // POST api/User/ChangePassword
            //    [Route("ChangePassword")]
            //    public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
            //    {
            //        if (!ModelState.IsValid)
            //        {
            //            return BadRequest(ModelState);
            //        }

            //        var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            //        IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword,
            //            model.NewPassword);

            //        if (!result.Succeeded)
            //        {
            //            return GetErrorResult(result);
            //        }

            //        return Ok();
            //    }

            //    // POST api/User/SetPassword
            //    [Route("SetPassword")]
            //    public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
            //    {
            //        if (!ModelState.IsValid)
            //        {
            //            return BadRequest(ModelState);
            //        }

            //        var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            //        IdentityResult result = await _userManager.AddPasswordAsync(user, model.NewPassword);

            //        if (!result.Succeeded)
            //        {
            //            return GetErrorResult(result);
            //        }

            //        return Ok();
            //    }


            //    #region Helpers

            //    //private IAuthenticationManager Authentication
            //    //{
            //    //    get { return Request.GetOwinContext().Authentication; }
            //    //}

            //    private IHttpActionResult GetErrorResult(IdentityResult result)
            //    {
            //        if (result == null)
            //        {
            //            return InternalServerError();
            //        }

            //        if (!result.Succeeded)
            //        {
            //            if (result.Errors != null)
            //            {
            //                foreach (var error in result.Errors)
            //                {
            //                    ModelState.AddModelError(error.Code, error.Description);
            //                }
            //            }

            //            if (ModelState.IsValid)
            //            {
            //                // No ModelState errors are available to send, so just return an empty BadRequest.
            //                return BadRequest();
            //            }

            //            return BadRequest(ModelState);
            //        }

            //        return null;
            //    }

            //    //public static string GetUserId(this ClaimsPrincipal principal)
            //    //{
            //    //    return principal.FindFirstValue(ClaimTypes.NameIdentifier);
            //    //}

            //    #endregion

            //}
        }

    public class LoginResult
    {
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public string UserId { get; set; }
        public DateTime Expires { get; set; }
        public bool IsValid { get; set; }
    }

}