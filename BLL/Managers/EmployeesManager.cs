using Core.Interfaces.Managers;
using Core.Models.API_Models;
using Core.Models.Models;
using Core.Result_Handler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class EmployeesManager : UserManager<UserModel>, IEmployeesManager<UserModel>
    {
        Func<UserModel, UserModel> linksNullifications = LinksNullifications;
        List<Exception> _exceptions = new List<Exception>();

        public EmployeesManager(
            IUserStore<UserModel> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserModel> passwordHasher,
            IEnumerable<IUserValidator<UserModel>> userValidators,
            IEnumerable<IPasswordValidator<UserModel>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<UserModel>> logger
            ) : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger
                )
        {
        }

        //public override async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        //{
        //    List<Claim> claims = new List<Claim>();
        //    roles.ToList().ForEach(r => claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

        //    await AddToRolesAsync(user, roles);
        //    await AddClaimsAsync(user, claims);

        //    return await UpdateAsync(user);
        //}

        //public override async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        //{

        //}

        public async Task<IEnumerable<UserModel>> GetAllEmployees()
        {
            _exceptions.Clear();
            try
            {
                IList<UserModel> users = await GetUsersInRoleAsync("User");
                if (users.Count == 0)
                    throw new Exception("Users in role \"Employee\" don`t exist");
                users = users.Select(u => linksNullifications(u)).ToList();
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
                {
                    throw new TimeTrakerResult(_exceptions);
                }
            }
        }

        public async Task<IEnumerable<UserModel>> GetActiveEmployees()
        {
            _exceptions.Clear();
            try
            {
                IList<UserModel> users = await GetUsersInRoleAsync("User");
                users = users
                    .Where(u => u.IsActive == true)
                    .ToList();
                users = users.Select(u => linksNullifications(u)).ToList();
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
                {
                    throw new TimeTrakerResult(_exceptions);
                }
            }
        }

        public async Task<IEnumerable<UserModel>> GetNonActiveEmployees()
        {
            _exceptions.Clear();
            try
            {
                IList<UserModel> users = await GetUsersInRoleAsync("User");
                users = users
                    .Where(u => u.IsActive == false)
                    .ToList();
                users = users.Select(u => linksNullifications(u)).ToList();
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
                {
                    throw new TimeTrakerResult(_exceptions);
                }
            }

        }

        public async Task<IEnumerable<UserModel>> GetUserWorkData()
        {
            _exceptions.Clear();
            DateTime dateNow = DateTime.Now.Date;
            IEnumerable<UserModel> employees;
            try
            {
                employees = Users
                    .Where(u => u.WorkSessionData.Any(wd => wd.StartWork.Date == dateNow))
                    .Include(u => u.WorkSessionData);

                employees = employees.Select(e =>
                {
                    e.WorkSessionData = e.WorkSessionData.Where(wd => wd.StartWork.Date == dateNow).ToList();
                    linksNullifications(e);
                    return e;
                });
                return employees;
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                return null;
            }
            finally
            {
                if (_exceptions.Count != 0)
                {
                    throw new TimeTrakerResult(_exceptions);
                }
            }
        }

        public async Task<IEnumerable<UserModel>> GetUserWorkData(IntervalModel interval)
        {
            _exceptions.Clear();
            DateTime dateNow = DateTime.Now.Date;
            IEnumerable<UserModel> employees;
            try
            {
                employees = Users
                    .Where(u => u.WorkSessionData.Any(wd => wd.StartWork >= interval.From && wd.StopWork <= interval.Till))
                    .Include(u => u.WorkSessionData);

                employees = employees.Select(e =>
                {
                    e.WorkSessionData = e.WorkSessionData.Where(wd => wd.StartWork >= interval.From && wd.StopWork <= interval.Till).ToList();
                    linksNullifications(e);
                    return e;
                });
                return employees;
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                return null;
            }
            finally
            {
                if (_exceptions.Count != 0)
                {
                    throw new TimeTrakerResult(_exceptions);
                }
            }
        }

        private static UserModel LinksNullifications(UserModel user)
        {
            user.UserGroups = null;
            user.WorkSessionData = user.WorkSessionData.Select(wd =>
            {
                wd.User = null;
                return wd;
            }).ToList();
            return user;
        }
    }
}
