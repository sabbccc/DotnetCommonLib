using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Configuration
{
    public class ApiKeySettings
    {
        public string HeaderName { get; set; } = "X-API-KEY";
        public string[] ValidKeys { get; set; } = Array.Empty<string>();
    }
}
