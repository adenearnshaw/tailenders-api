using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TailendersApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DateTime.UtcNow);
        }
    }
}