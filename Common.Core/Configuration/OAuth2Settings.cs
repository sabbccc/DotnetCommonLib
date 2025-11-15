using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Configuration
{
    public class OAuth2Settings
    {
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string AuthorizationEndpoint { get; set; } = "";
        public string TokenEndpoint { get; set; } = "";
        public string UserInfoEndpoint { get; set; } = "";
        public string? CallbackPath { get; set; }
    }
}
