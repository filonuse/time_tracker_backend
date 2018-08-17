using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Managers;
using Core.Interfaces.Managers;
using Core.Interfaces.Repositories;
using Core.Models.API_Models;
using Core.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeTraker.Controllers
{
    [Produces("application/json")]
    [Route("admin/reports")]
    //[Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        IEmployeesManager<UserModel> _employeesManager;
        IGenericRepository<UserWorkSessionDataModel> _repository;

        public ReportsController(IEmployeesManager<UserModel> employeesManager, IGenericRepository<UserWorkSessionDataModel> repository)
        {
            _employeesManager = employeesManager;
            _repository = repository;
        }

        //========== Test ============
        [HttpGet]
        public async Task<IActionResult> Add(int stw, int spw, string id)
        {
            var startWork = DateTime.Now.AddHours(stw);
            var stopWork = startWork.AddHours(spw);
            var workDuration = stopWork.Subtract(startWork);
            var newData = new UserWorkSessionDataModel { StartWork = startWork, StopWork = stopWork, WorkDuration = workDuration, UserId = id };

            _repository.Insert(newData);
            return Ok(newData);
        }
        //============================

        [HttpGet]
        [Route("daily_report")]
        public async Task<IActionResult> DailyReport()
        {
            IEnumerable<UserModel> userData = await _employeesManager.GetUserWorkData();
            var res = userData.Select(ud => new
            {
                first_name = ud.FirstName,
                last_name = ud.LastName,
                nickname = ud.UserName,
                work_direction = ud.WorkDirection,
                work_data = ud.WorkSessionData?.Select(wd => new
                {
                    start_work = wd.StartWork,
                    work_duration = wd.WorkDuration,
                    stop_work = wd.StopWork
                })
            });
            return Ok(res);
        }

        [HttpPost]
        [Route("interval_report")]
        public async Task<IActionResult> IntervalReport([FromBody]IntervalModel interval)
        {
            IEnumerable<UserModel> userData = await _employeesManager.GetUserWorkData(interval);
            var res = userData.Select(ud => new
            {
                user_name = ud.UserName,
                work_direction = ud.WorkDirection,
                work_data = ud.WorkSessionData.Select(wd => new
                {
                    start_work = wd.StartWork,
                    work_duration = wd.WorkDuration,
                    stop_work = wd.StopWork
                })
            });
            return Ok(res);
        }
    }
}