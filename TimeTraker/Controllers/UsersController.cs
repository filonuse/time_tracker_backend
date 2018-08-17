using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Managers;
using Core.Interfaces.Managers;
using Core.Models.API_Models;
using Core.Models.Models;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTraker.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTraker.Controllers
{
    [Produces("application/json")]
    [Route("users")]
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private List<Claim> _claims = new List<Claim>();
        
        private readonly JwtTokenService _tokenFabric;
        private readonly IDataUpdateManager<string> _dataManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserModel> _userManager;

        public UsersController(JwtTokenService tokenFabric, IDataUpdateManager<string> dataManager, RoleManager<IdentityRole> roleManager, UserManager<UserModel> manager)
        {
            _tokenFabric = tokenFabric;
            _dataManager = dataManager;
            _roleManager = roleManager;
            _userManager = manager;
        }

        [HttpPost]
        [Route("log_in")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody]UserLoginApiModel user)
        {
            try
            {
                var _user = await _userManager.FindByEmailAsync(user.Email);
                if (_user == null)
                    return StatusCode(204, $"User {user.Email} doesn`t found");
                var confirmPassword = _userManager.PasswordHasher.VerifyHashedPassword(_user, _user.PasswordHash, user.Password);
                if (confirmPassword == PasswordVerificationResult.Failed)
                    return StatusCode(401, "Password doesn`t match");
                var token = _tokenFabric.GetToken(_user).Result;

                var response = new
                {
                    user_id = _user.Id,
                    acces_token = token
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                IEnumerable<UserModel> users = _userManager.Users.Include(u => u.Claims).ToList();
                if (users.Count() == 0)
                    return StatusCode(204, $"Users are not found"); ;
                IEnumerable<UserViewModel> responseModels = users.Select(u => new UserViewModel(u));

                return Ok(responseModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("get_user")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                UserModel user = _userManager.Users.Where(u => u.Id == id).Include(u => u.Claims).FirstOrDefault();
                if (User == null)
                    return StatusCode(204, $"User is not found"); ;
                UserViewModel responseModel = new UserViewModel(user);

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("get_by_bitrix")]
        public async Task<IActionResult> GetUserByBitrixId(int bitrixId)
        {
            try
            {
                UserModel user = _userManager.Users.Where(u => u.BitrixId == bitrixId).Include(u => u.Claims).FirstOrDefault();
                if (user == null)
                    return StatusCode(204, $"User is not found");
                UserViewModel responseModel = new UserViewModel(user);

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create_user")]
        public async Task<IActionResult> CreateUserProfile([FromBody]UserCreationApiModel user)
        {
            _claims.Clear();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList());

                // Creating a new User.
                UserModel newUser = new UserModel(user);
                await _userManager.CreateAsync(newUser, user.Password);

                // Adding User to Roles.
                IEnumerable<IdentityRole> addedRoles = new List<IdentityRole>(_roleManager.Roles.Where(r => user.RoleIDs.Any(id => id == r.Id)));
                await _userManager.AddToRolesAsync(newUser, addedRoles.Select(r => r.Name));

                // Adding a new Claims.
                addedRoles.ToList().ForEach(r => _claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, r.Name)));
                IdentityResult result = await _userManager.AddClaimsAsync(newUser, _claims);

                if (result.Succeeded)
                    return Ok($"User {user.UserName} was created.");
                else
                    return BadRequest(result.Errors.Select(er => er.Description));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //================================== Tests  =======================================
        [HttpPost]
        [Route("create_users")]
        public async Task<IActionResult> CreateManyUserProfiles([FromBody]List<UserCreationApiModel> users)
        {
            foreach (var user in users)
            {
                await CreateUserProfile(user);
            }
            return Ok("Готово");
        }

        [HttpGet]
        [Route("get_user_ids")]
        public async Task<IActionResult> GetIDs()
        {
            var userIDs = _userManager.Users.Select(u => u.Id).ToList();
            return Ok(userIDs);
        }
        //================================ End test =======================================

        [HttpPut]
        [Route("update_user")]
        public async Task<IActionResult> ChangeUserData(string id, [FromBody]UserChangeApiModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errors);
                }

                var _user = await _userManager.FindByIdAsync(id);
                var confirmPassword = _userManager.PasswordHasher.VerifyHashedPassword(_user, _user.PasswordHash, user.Password);
                if (confirmPassword == PasswordVerificationResult.Failed)
                    return StatusCode(401, "Password doesn`t match");

                await ChangeUserData(_user, user);
                await ChangeUserRoles(_user, user.RoleNames);

                return Ok($"User {user.UserName} was updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete_user")]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            try
            {
                var _user = await _userManager.FindByIdAsync(id);
                if (_user == null)
                    return BadRequest("Profile not found");

                await _userManager.DeleteAsync(_user);
                return Ok($"User {_user.FirstName} was delete");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task ChangeUserData(UserModel _user, UserChangeApiModel user)
        {
            _user.UserName = user.UserName;
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.Email = user.Email;
            _user.PhoneNumber = user.PhoneNumber;
            _user.WorkDirection = user.WorkDirection;

            _user.UpdateDate = DateTime.Now;
            await _userManager.UpdateAsync(_user);
        }

        private async Task ChangeUserRoles(UserModel user, IEnumerable<string> roleNames)
        {
            _claims.Clear();
            IEnumerable<string> userRoles = await _userManager.GetRolesAsync(user);
            IEnumerable<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            _dataManager.Update(roleNames, userRoles);

            // Change user roles.
            await _userManager.RemoveFromRolesAsync(user, _dataManager.NonActualItems);
            await _userManager.AddToRolesAsync(user, _dataManager.NewItems);

            // Change user Claims.
            _dataManager.NewItems.ToList().ForEach(i => _claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, i)));
            IEnumerable<Claim> removeClaims = userClaims.Where(uc => _dataManager.NonActualItems.Any(i => i == uc.Value));
            await _userManager.RemoveClaimsAsync(user, removeClaims);
            await _userManager.AddClaimsAsync(user, _claims);
        }

    }
}
