using Core.Interfaces.Managers;
using Core.Interfaces.Repositories;
using Core.Models.Models;
using Core.Models.Models.Bind_Models;
using Core.Result_Handler;
using DaL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class GroupsManager : IGroupsManager<GroupModel, UserModel>
    {
        private List<Exception> _exceptions = new List<Exception>();

        private readonly AppDbContext _context;
        private readonly DateTime _updateDate;
        private readonly IGenericRepository<GroupModel> _groupRepository;
        private readonly IGenericRepository<UserGroupModel> _userGroupRepository;
        private readonly UserManager<UserModel> _userManager;

        public GroupsManager(
            IGenericRepository<GroupModel> groupRepository,
            IGenericRepository<UserGroupModel> userGroupRepository,
            UserManager<UserModel> userManager, AppDbContext context
            )
        {
            _context = context;
            _updateDate = DateTime.Now;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
            _userManager = userManager;
        }

        public async Task AddManagers(GroupModel group, IEnumerable<string> managerIDs)
        {
            _exceptions.Clear();

            try
            {
                managerIDs.ToList().ForEach(id => group.UserGroups.Add(new UserGroupModel { UserId = id, GroupId = group.Id, IsManager = true }));
                group.UpdateDate = _updateDate;
                _groupRepository.Update(group);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task AddEmployees(GroupModel group, IEnumerable<string> employeeIDs)
        {
            _exceptions.Clear();

            try
            {
                employeeIDs.ToList().ForEach(id => group.UserGroups.Add(new UserGroupModel { UserId = id, GroupId = group.Id }));
                group.UpdateDate = _updateDate;
                _groupRepository.Update(group);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task ChangeName(GroupModel group, string name)
        {
            _exceptions.Clear();

            try
            {
                group.GroupName = name;
                group.UpdateDate = _updateDate;
                _groupRepository.Update(group);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task CreateGroup(string groupName, IEnumerable<string> groupManagerIDs, IEnumerable<string> groupEmployeeIDs)
        {
            _exceptions.Clear();

            try
            {
                GroupModel group = new GroupModel { GroupName = groupName };
                if (groupManagerIDs.Count() != 0)
                    groupManagerIDs.ToList().ForEach(id => group.UserGroups.Add(new UserGroupModel { UserId = id, GroupId = group.Id, IsManager = true }));
                if (groupEmployeeIDs.Count() != 0)
                    groupEmployeeIDs.ToList().ForEach(id => group.UserGroups.Add(new UserGroupModel { UserId = id, GroupId = group.Id }));

                _groupRepository.Insert(group);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task DeleteGroup(GroupModel group)
        {
            _exceptions.Clear();

            try
            {
                await _groupRepository.Delete(group);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task DeleteManagers(GroupModel group, IEnumerable<string> managerIDs)
        {
            _exceptions.Clear();

            try
            {
                if (!_context.Entry(group).Collection(g => g.UserGroups.Where(ug => ug.IsManager)).IsLoaded)
                    _context.Entry(group).Collection(g => g.UserGroups.Where(ug => ug.IsManager)).Load();

                managerIDs.ToList().ForEach(id =>
                {
                    UserGroupModel groupManager = group.UserGroups.FirstOrDefault(mg => mg.UserId == id);
                    group.UserGroups.Remove(groupManager);
                });

                group.UpdateDate = _updateDate;
                _groupRepository.Update(group);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task DeleteEmployees(GroupModel group, IEnumerable<string> employeeIDs)
        {
            _exceptions.Clear();

            try
            {
                if (!_context.Entry(group).Collection(g => g.UserGroups.Where(ug => !ug.IsManager)).IsLoaded)
                    _context.Entry(group).Collection(g => g.UserGroups.Where(ug => !ug.IsManager)).Load();

                employeeIDs.ToList().ForEach(id =>
                {
                    UserGroupModel groupEmployee = group.UserGroups.FirstOrDefault(ug => ug.UserId == id);
                    group.UserGroups.Remove(groupEmployee);
                });

                group.UpdateDate = _updateDate;
                _groupRepository.Update(group);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task<GroupModel> FindById(string id)
        {
            _exceptions.Clear();

            try
            {
                var group = _groupRepository.Table
                    .Where(g => g.Id == id)
                    .Include(g => g.UserGroups)
                    .FirstOrDefault();
                return group;
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                return null;
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task<IEnumerable<GroupModel>> GetAllGroups()
        {
            _exceptions.Clear();

            try
            {
                IEnumerable<GroupModel> groups = _groupRepository.Table
                    .Include(g => g.UserGroups)
                    .ToList();

                return groups;
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                return null;
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task<IEnumerable<UserModel>> GetManagers(GroupModel group)
        {
            _exceptions.Clear();

            try
            {
                if (!_context.Entry(group).Collection(g => g.UserGroups.Where(ug => ug.IsManager)).IsLoaded)
                    _context.Entry(group).Collection(g => g.UserGroups.Where(ug => ug.IsManager)).Load();

                IEnumerable<UserModel> managers = group.UserGroups.Where(ug => ug.IsManager).Select(ug => ug.User);
                return managers;
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                return null;
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }

        public async Task<IEnumerable<UserModel>> GetEmployees(GroupModel group)
        {
            _exceptions.Clear();

            try
            {
                if (!_context.Entry(group).Collection(g => g.UserGroups.Where(ug => !ug.IsManager)).IsLoaded)
                    _context.Entry(group).Collection(g => g.UserGroups.Where(ug => !ug.IsManager)).Load();

                IEnumerable<UserModel> users = group.UserGroups.Where(ug => !ug.IsManager).Select(ug => ug.User);
                return users;
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                return null;
            }
            finally
            {
                if (_exceptions.Count != 0)
                    throw new TimeTrakerResult(_exceptions);
            }
        }
    }
}
