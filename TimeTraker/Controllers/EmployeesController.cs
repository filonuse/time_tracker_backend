using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Managers;
using Core.Interfaces.Managers;
using Core.Models.Models;
using Core.Result_Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TimeTraker.Controllers
{
    [Produces("application/json")]
    [Route("admin/employess")]
    //[Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        readonly IEmployeesManager<UserModel> _employeesManager;

        public EmployeesController(IEmployeesManager<UserModel> employeesManager)
        {
            _employeesManager = employeesManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Employees()
        {
            try
            {
                IEnumerable<UserModel> users = await _employeesManager.GetAllEmployees();
                if (users.Count() == 0)
                    return NoContent();
                return Ok(users);
            }
            catch(TimeTrakerResult ex)
            {
                return BadRequest(ex.ExeptionMessages);
            }
        }

        [HttpGet]
        [Route("online")]
        public async Task<IActionResult> ActiveEmployees()
        {
            IEnumerable<UserModel> users = await _employeesManager.GetActiveEmployees();
            if (users.Count() == 0)
                return NoContent();
            return Ok(users);
        }

        [HttpGet]
        [Route("offline")]
        public async Task<IActionResult> NonActiveEmployees()
        {
            IEnumerable<UserModel> users = await _employeesManager.GetNonActiveEmployees();
            if (users.Count() == 0)
                return NoContent();
            return Ok(users);
        }
    }
}