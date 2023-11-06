using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Darwin.Service.Helper
{
    internal class LinkCreator : ILinkCreator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public LinkCreator(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        public async Task<string> CreateTokenMailUrl(string action, string controller, string userId, string token)
        {
            var requestScheme=_httpContextAccessor.HttpContext!.Request.Scheme;
            var apiHost=_httpContextAccessor.HttpContext.Request.Host;
            return _linkGenerator.GetUriByAction(action, controller, new { userId = userId, token = token }, requestScheme, apiHost)!;
        }

    }
}
