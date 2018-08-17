using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.API_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTraker.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var result = await Task.FromResult(roles);
            if (!result.Any())
                return NoContent();
            return Ok(result);
        }

        [HttpGet]
        [Route("getrole")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NoContent();
            return Ok(role);
        }

        [HttpPost]
        [Route("createrole")]
        public async Task<IActionResult> CreateRole([FromBody]RoleCreationApiModel role)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            var newRole = new IdentityRole(role.RoleName);
            var roleCreated = await _roleManager.CreateAsync(newRole);
            if (roleCreated.Succeeded)
            {
                return Ok($"Role {role.RoleName} was created");
            }
            else
            {
                var result = roleCreated.Errors.ToList();
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("deleterole")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NoContent();
            var roleDeleted = await _roleManager.DeleteAsync(role);
            if (roleDeleted.Succeeded)
            {
                return Ok($"Role {role.Name} was deleted.");
            }
            else
            {
                var result = roleDeleted.Errors.ToList();
                return BadRequest(result);
            }
        }
    }
}
