using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Interfaces.Repositories
{
    public interface IFileRepository<TFileModel> where TFileModel: class
    {
        /// <summary>
        /// Get all media files by file model objects.
        /// </summary>
        /// <returns>Task<IEnumerable<string>> base64string</returns>
        Task<IEnumerable<string>> GetByPath(IEnumerable<TFileModel> files);

        /// <summary>
        /// Add file to filesystem.
        /// </summary>
        /// <param name="fileName">Name of the adding file.</param>
        /// <param name="imageBites">Massive of the bytes of the file.</param>
        /// <returns>void</returns>
        bool InsertFile(TFileModel file, string base64string);
        
        /// <summary>
        /// Deleting a collection of files.
        /// </summary>
        /// <param name="files">Files to delete.</param>
        /// <returns>Task</returns>
        Task DeleteFiles(IEnumerable<TFileModel> files);
    }
}
