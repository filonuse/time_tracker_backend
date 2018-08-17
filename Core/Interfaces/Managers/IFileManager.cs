using Core.Models.API_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Managers
{
    public interface IFileManager<TFileModel> where TFileModel: class
    {
        /// <summary>
        /// Deleting entities with file info. Deleting files from the directory.
        /// </summary>
        /// <param name="files">Collection of file objects.</param>
        /// <returns></returns>
        Task DeleteFiles(IEnumerable<string> fileIDs);

        /// <summary>
        /// Get files for a define period.
        /// </summary>
        /// <param name="user">User object, who`s files would be unload.</param>
        /// <param name="interval">Interval for sorting files by creation time.</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetFilesForUser(string userId, DateTime fromPeriod, DateTime tillPeriod);

        /// <summary>
        /// Creating entity with file info and save file to the directory.
        /// </summary>
        /// <param name="uoloadFile">File object wich will saved</param>
        /// <returns></returns>
        Task<IEnumerable<TFileModel>> AddFile(string userId, LogApiModel file);
    }
}