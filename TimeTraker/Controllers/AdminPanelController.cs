using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.API_Models;
using Core.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeTraker.Services;

namespace TimeTraker.Controllers
{
    [Route("admin")]
    //[Authorize(Roles = "Admin,Superadmin")]
    public class AdminPanelController : Controller
    {
        UserManager<UserModel> _manager;

        public AdminPanelController(UserManager<UserModel> manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Review()
        {
            return Ok();
        }

        [HttpPost]
        [Route("create_employee")]
        public async Task<IActionResult> CreateEmployee()
        {
            return NoContent();
        }
    }
}