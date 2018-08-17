using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Models.Models
{
    public class FileModel : BaseEntity
    {
        public FileModel() { }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime CreationDate { get; set; }

        #region Relations
        // Enable many-to-one relation with LogModel.
        public string LogModelId { get; set; }
        public virtual LogModel LogModel { get; set; }
        #endregion
    }
}