using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace Darwin.AuthServer.Helper;

public sealed class LinkCreator : ILinkCreator
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    public LinkCreator(IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }

    public async Task<string> CreateTokenMailUrl(string action, string controller, string userId, string token)
    {
        var requestScheme=_httpContextAccessor.HttpContext!.Request.Scheme;
        var apiHost=_httpContextAccessor.HttpContext.Request.Host;
        var response= _linkGenerator.GetUriByAction(action, controller, new { userId = userId, token = token }, requestScheme, apiHost)!;
       
        return response;
    }
}