using Core.Interfaces.Managers;
using Core.Interfaces.Repositories;
using Core.Models.API_Models;
using Core.Models.Models;
using Core.Result_Handler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class FileManager : IFileManager<FileModel>
    {
        private List<Exception> _exceptions = new List<Exception>();

        private readonly IGenericRepository<FileModel> _dbRepository;
        private readonly IFileRepository<FileModel> _fileRepository;
        private readonly UserManager<UserModel> _manager;

        public FileManager(IGenericRepository<FileModel> dbRepo, IFileRepository<FileModel> fileRepo, UserManager<UserModel> manager)
        {
            _dbRepository = dbRepo;
            _fileRepository = fileRepo;
            _manager = manager;
        }

        public async Task DeleteFiles(IEnumerable<string> fileIDs)
        {
            _exceptions.Clear();

            try
            {
                IEnumerable<FileModel> files = fileIDs.Select(fileId => _dbRepository.GetById(fileId));

                await _fileRepository.DeleteFiles(files);
                await _dbRepository.Delete(files);
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

        public async Task<IEnumerable<string>> GetFilesForUser(string userId, DateTime fromPeriod, DateTime tillPeriod)
        {
            _exceptions.Clear();

            try
            {
                UserModel _user = _manager.Users
                    .Where(u => u.Id == userId)
                    .Include(user => user.Logs.Where(log => (log.LogTime >= fromPeriod && log.LogTime <= tillPeriod)))
                    .ThenInclude(log => log.Screenshots)
                    .FirstOrDefault();

                IEnumerable<string> base64string = await _fileRepository.GetByPath(_user.Logs.SelectMany(l => l.Screenshots));
                return base64string;
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

        public Task<IEnumerable<FileModel>> AddFile(string userId, LogApiModel file)
        {
            throw new NotImplementedException();
        }
    }
}