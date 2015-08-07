using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyCash.Models
{
    public enum InfoLogType
    {
        INFO = 1,
        SUCCESS = 2,
        WARNING = 3,
        ERROR = 4
    };

    public class InfologViewModel
    {
        public InfoLogType LogType { get; set; }
        public string Message { get; set; }
    }
}