using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Managers
{
    public interface IGroupsManager<TGroupModel, TUserModel>
        where TGroupModel : class
        where TUserModel : class
    {

        /// <summary>
        /// Adding managers to an existing group.
        /// </summary>
        /// <param name="group">Group to adding managers.</param>
        /// <param name="manager">Enumeration of users, who will added to group as managers.</param>
        /// <returns>Task</returns>
        Task AddManagers(TGroupModel group, IEnumerable<string> managerIDs);

        /// <summary>
        /// Adding a employees to an existing group.
        /// </summary>
        /// <param name="group">Group to adding employees.</param>
        /// <param name="users">Enumeration of employees to be added to the group.</param>
        /// <returns>Task</returns>
        Task AddEmployees(TGroupModel group, IEnumerable<string> employeeIDs);

        /// <summary>
        /// Changing the group name.
        /// </summary>
        /// <param name="group">The group whose name should be updated.</param>
        /// <param name="name">New group name</param>
        /// <returns>Task</returns>
        Task ChangeName(TGroupModel group, string name);

        /// <summary>
        /// Create a new group.
        /// </summary>
        /// <param name="groupName">The name of new group.</param>
        /// <param name="groupManagerIDs">Enumeration of adding managers IDs.</param>
        /// <param name="groupEmployeeIDs">Enumeration of adding employees IDs.</param>
        /// <returns>Task</returns>
        Task CreateGroup(string groupName, IEnumerable<string> groupManagerIDs, IEnumerable<string> groupUserIDs);

        /// <summary>
        /// Delete group
        /// </summary>
        /// <param name="group">Object of Group class for deleting a group.</param>
        /// <returns>Task</returns>
        Task DeleteGroup(TGroupModel group);

        /// <summary>
        /// Remove managers from the group.
        /// </summary>
        /// <param name="group">Group to delete manager.</param>
        /// <param name="managers">Enumeration of managers, who will deleted from group.</param>
        /// <returns>Task</returns>
        Task DeleteManagers(TGroupModel group, IEnumerable<string> managerIDs);

        /// <summary>
        /// Remove employees from the group.
        /// </summary>
        /// <param name="group">Group to delete the user.</param>
        /// <param name="employees">Enumeration of emloyees, who will deleted from group.</param>
        /// <returns>Task</returns>
        Task DeleteEmployees(TGroupModel group, IEnumerable<string> employeeIDs);

        /// <summary>
        /// Find Group by ID.
        /// </summary>
        /// <param name="id">ID of the group.</param>
        /// <returns>Task<TGroupModel></returns>
        Task<TGroupModel> FindById(string id);

        /// <summary>
        /// Returns all group.
        /// </summary>
        /// <returns>Task<IEnumerable<TGroupModel>></returns>
        Task<IEnumerable<TGroupModel>> GetAllGroups();

        /// <summary>
        /// Get all group managers.
        /// </summary>
        /// <param name="group">Group to get manager.</param>
        /// <returns>Task</returns>
        Task<IEnumerable<TUserModel>> GetManagers(TGroupModel group);

        /// <summary>
        /// Get all group employees.
        /// </summary>
        /// <param name="group">Group to get users.</param>
        /// <returns>Task</returns>
        Task<IEnumerable<TUserModel>> GetEmployees(TGroupModel group);
    }
}
