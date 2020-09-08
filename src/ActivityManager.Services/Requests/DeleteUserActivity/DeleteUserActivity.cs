﻿using System.Security.Claims;
using MediatR;

namespace ActivityManager.Services
{
    public class DeleteUserActivity : IRequest
    {
        public ClaimsPrincipal User { get; set; }
        public int ActivityId { get; set; }
    }
}
