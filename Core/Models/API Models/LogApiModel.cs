using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.API_Models
{
    public class LogApiModel
    {
        public LogApiModel() { }

        public string UserId { get; set; }
        public DateTime LogTime { get; set; }                   // Time when this log was created.
        public DateTime UsefulTime { get; set; }                // Duration of useful work.
        public DateTime UnusefulTime { get; set; }              // Duration of unuseful work.
        public IEnumerable<ProcessInfoApiModel> OpenApps { get; set; }          // Information about opened applications.
        public IEnumerable<ScreenshotApiModel> Screenshots { get; set; }        // List of screenshots.
    }
}
