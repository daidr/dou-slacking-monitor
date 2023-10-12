using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomlyn.Model;

namespace DouSlackingMonitor.Models
{
    public class Config
    {
            public string Secret { get; set; }
            public string ApiEntrypoint { get; set; }
            public TomlArray ProcessWhitelist { get; set; }
    }
}
