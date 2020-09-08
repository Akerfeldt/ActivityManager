using System.Collections.Generic;
using ActivityManager.Blazor.Server.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ActivityManager.Blazor.Server.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OidcConfigurationController> _logger;

        public OidcConfigurationController(ILogger<OidcConfigurationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("_configuration/{clientId}")]
        public ActionResult<Dictionary<string, string>> GetClientRequestParameters([FromRoute] string clientId)
        {
            var config = _configuration.GetSection("ClientRequestParameters").Get<ClientRequestParameters>();

            var result = new Dictionary<string, string>
            {
                { "authority", config.Authority },
                { "client_id", config.ClientId },
                { "post_logout_redirect_uri", config.PostLogoutRedirectUri },
                { "redirect_uri", config.RedirectUri },
                { "response_type", config.ResponseType },
                { "scope", config.Scope }
            };

            return Ok(result);
        }
    }
}
