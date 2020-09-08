using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityManager.Blazor.Server.Configuration
{
    public class ClientRequestParameters
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string RedirectUri { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }
    }
}
