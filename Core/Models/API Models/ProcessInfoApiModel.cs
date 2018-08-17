using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.API_Models
{
    public class ProcessInfoApiModel
    {
        public ProcessInfoApiModel() { }

        public string ProcessTitle { get; set; }
        public string ProcessName { get; set; }
        public bool IsMain { get; set; }
    }
}
