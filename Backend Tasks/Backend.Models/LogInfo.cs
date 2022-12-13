using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class LogInfo
    {
        public string Error { get; set; }

        public string Class { get; set; }
        public string Function { get; set; }
        public string IP { get; set; }

        public string Message
        { get
            {
                return string.Format("{0}/{1} - {2}: {3}", Class, Function, IP, Error);
            }
        }
    }
}
