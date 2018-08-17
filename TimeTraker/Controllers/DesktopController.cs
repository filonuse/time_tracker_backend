using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Managers;
using Core.Models.API_Models;
using Core.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TimeTraker.Controllers.Desktop
{
    [Produces("application/json")]
    [Route("desktop_api")]
    //[Authorize]
    public class DesktopController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IFileManager<FileModel> _fileManager;

        public DesktopController(UserManager<UserModel> userManager, IFileManager<FileModel> fileManager)
        {
            _userManager = userManager;
            _fileManager = fileManager;
        }

        [HttpPost]
        [Route("create_log")]
        public async Task<IActionResult> TakeLog([FromBody] LogApiModel log)
        {
            return StatusCode(503);
        }

        [HttpPost]
        [Route("create_logs")]
        public async Task<IActionResult> TakeLogs([FromBody] IEnumerable<LogApiModel> logs)
        {
            return StatusCode(503);
        }

        [HttpPost]
        [Route("change_user_activity")]
        public async Task<IActionResult> CahgeActivity([FromBody] ChangeUserActivity model)
        {
            return StatusCode(503);
        }
    }
}