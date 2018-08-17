using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Managers;
using Core.Models.API_Models;
using Core.Models.Models;
using Core.Models.ViewModels;
using Core.Result_Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TimeTraker.Controllers
{
    [Produces("application/json")]
    [Route("admin/groups")]
    //[Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        private readonly IDataUpdateManager<string> _dataManager;
        private readonly IGroupsManager<GroupModel, UserModel> _groupsManager;
        private readonly UserManager<UserModel> _userManager;

        public GroupsController(IDataUpdateManager<string> dataManager, IGroupsManager<GroupModel, UserModel> groupsManager, UserManager<UserModel> userManager)
        {
            _dataManager = dataManager;
            _groupsManager = groupsManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            { 
                IEnumerable<GroupModel> groups = await _groupsManager.GetAllGroups();
                if (groups == null)
                    return NoContent();

                IEnumerable<GroupViewModel> responseModels = groups.Select(g => new GroupViewModel(g));

                return Ok(responseModels);
            }
            catch(TimeTrakerResult res)
            {
                return StatusCode(500, res.ExeptionMessages);
            }
        }

        [HttpGet]
        [Route("group")]
        public async Task<IActionResult> GetGroupById(string id)
        {
            try
            {
                GroupModel group = await _groupsManager.FindById(id);
                if (group == null)
                    return Ok("Group not found");

                GroupViewModel responseModel = new GroupViewModel(group);

                return Ok(responseModel);
            }
            catch (TimeTrakerResult res)
            {
                return StatusCode(500, res.ExeptionMessages);
            }
        }

        [HttpPost]
        [Route("create_group")]
        public async Task<IActionResult> CreateGroup([FromBody]GroupCreationApiModel group)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errors);
                }

                await _groupsManager.CreateGroup(group.GroupName, group.ManagerIDs, group.UserIDs);
                return Ok($"Group {group.GroupName} was created");
            }
            catch (TimeTrakerResult res)
            {
                return StatusCode(500, res.ExeptionMessages);
            }
        }

        [HttpPut]
        [Route("update_group")]
        public async Task<IActionResult> UpdateGroup(string id, [FromBody]GroupChangeApiModel group)
        {
            try
            {
                GroupModel _group = await _groupsManager.FindById(id);
                IEnumerable<string> existManagerIDs = _group.UserGroups.Where(ug => ug.IsManager).Select(eg => eg.UserId);
                IEnumerable<string> existEmployeeIDs = _group.UserGroups.Where(ug => !ug.IsManager).Select(eg => eg.UserId);

                // Updating mangers.
                _dataManager.Update(group.UpdatedManagerIDs, existEmployeeIDs);
                await _groupsManager.DeleteManagers(_group, _dataManager.NonActualItems);
                await _groupsManager.AddManagers(_group, _dataManager.NewItems);

                // Updating Employees.
                _dataManager.Update(group.UpdatedEmployeeIDs, existEmployeeIDs);
                await _groupsManager.DeleteEmployees(_group, _dataManager.NonActualItems);
                await _groupsManager.AddEmployees(_group, _dataManager.NewItems);

                // Changing group name.
                await _groupsManager.ChangeName(_group, group.GroupName);

                return Ok($"Group {_group.GroupName} was updated.");
            }
            catch (TimeTrakerResult res)
            {
                return StatusCode(500, res.ExeptionMessages);
            }
        }

        [HttpDelete]
        [Route("delete_group")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            try
            {
                GroupModel group = await _groupsManager.FindById(id);
                if (group == null)
                    return Ok("Group not found");

                await _groupsManager.DeleteGroup(group);

                return Ok($"Group {group.GroupName} was deleted");
            }
            catch (TimeTrakerResult res)
            {
                return StatusCode(500, res.ExeptionMessages);
            }
        }
    }
}