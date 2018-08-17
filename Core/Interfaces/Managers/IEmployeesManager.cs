using Core.Models.API_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Managers
{
    public interface IEmployeesManager<TUserModel> where TUserModel: class
    {
        /// <summary>
        /// Get all users in role "Employee".
        /// </summary>
        /// <returns>Task<IEnumerable<TUserModel>> users.</returns>
        Task<IEnumerable<TUserModel>> GetAllEmployees();

        /// <summary>
        /// Get active users (field "isActive": true) in role "Employee".
        /// </summary>
        /// <returns>Task<IEnumerable<TUserModel>> users.</returns>
        Task<IEnumerable<TUserModel>> GetActiveEmployees();

        /// <summary>
        /// Get non active users (field "isActive": false) in role "Employee".
        /// </summary>
        /// <returns>Task<IEnumerable<TUserModel>> users.</returns>
        Task<IEnumerable<TUserModel>> GetNonActiveEmployees();

        /// <summary>
        /// Get user statistics from the beginning of the current day until the method is called.
        /// </summary>
        /// <returns>Task<IEnumerable<TUserModel>> user statistic.</returns>
        Task<IEnumerable<TUserModel>> GetUserWorkData();

        /// <summary>
        /// Get user statistics for a given interval.
        /// </summary>
        /// <param name="interval">Interval model. Has two fields: DateTime From, DateTime Till.</param>
        /// <returns>>Task<IEnumerable<TUserModel>> user statistic.</returns>
        Task<IEnumerable<TUserModel>> GetUserWorkData(IntervalModel interval);
    }
}
