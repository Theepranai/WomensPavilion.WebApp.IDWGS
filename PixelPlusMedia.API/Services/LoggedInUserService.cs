﻿using PixelPlusMedia.Application.Contracts;
using System.Security.Claims;

namespace PixelPlusMedia.API.Services;

public class LoggedInUserService : ILoggedInUserService
{
    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string UserId { get; }
}
