using Core.Interfaces.Repositories;
using Core.Models.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public class FileRepository : IFileRepository<FileModel>
    {
        // Set path to the media files directory.
        readonly string pathToMediaFiles = @"~/../media/screenshots";

        public async Task DeleteFiles(IEnumerable<FileModel> files)
        {
            try
            {
                files.ToList().ForEach(file =>
                {
                    string path = GetPathToFile(file.FileName, file.FileType);
                    File.Delete(path);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<string>> GetByPath(IEnumerable<FileModel> files)
        {
            List<string> images = new List<string>();

            try
            {
                files.ToList().ForEach(async file =>
                {
                    string path = GetPathToFile(file.FileName, file.FileType);
                    byte[] imageBytes;

                    using (FileStream stream = File.OpenRead(path))
                    {
                        imageBytes = new byte[stream.Length];
                        await stream.ReadAsync(imageBytes, 0, (int)stream.Length);
                    }

                    var image = Convert.ToBase64String(imageBytes);
                    images.Add(image);
                });
                return images;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool InsertFile(FileModel file, string base64string)
        {
            try
            {
                string path = GetPathToFile(file.FileName, file.FileType);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (FileStream stream = File.Create(path))
                {
                    byte[] imageBytes = Convert.FromBase64String(base64string);
                    stream.Write(imageBytes, 0, imageBytes.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        string GetPathToFile(string name, string ext)
        {
            char[] arr = name.ToCharArray();

            string nameToPathTemplate = $@"/{arr[0]}/{arr[1]}/{arr[2]}/{name}.{ext}";
            string path = pathToMediaFiles + nameToPathTemplate;

            return path;
        }
    }
}