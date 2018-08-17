using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.API_Models
{
    public class ScreenshotApiModel
    {
        public ScreenshotApiModel() { }

        public string Image { get; set; }           // Base64 file string.
        public string Type { get; set; }            // Type of file (for example .jpg)
        public string Name { get; set; }            // Name of file.
    }
}
