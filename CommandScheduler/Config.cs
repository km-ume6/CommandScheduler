using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler
{
    class Config
    {
        public string ServicePath { get; set; } = string.Empty;
        public string ServiceArg { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceDesc { get; set; } = string.Empty;
        public Config() { }
    }
}
