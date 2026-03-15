/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

using Microsoft.AspNetCore.Mvc;
using RealEstatePro.Application.Abstractions;

namespace RealEstatePro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public HealthController(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new 
            { 
                Status = "ok",
                CurrentTime = _dateTimeProvider.UtcNow 
            });
        }
    }
}
