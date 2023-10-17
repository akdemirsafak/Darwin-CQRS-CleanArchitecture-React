﻿using Microsoft.AspNetCore.Http;

namespace Darwin.Service.UserHelper;

public class CurrentUser : ICurrentUser
{
    private IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
    public int UserAge => Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("birth-date").Value);

}