using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS01.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            return new List<string>()
            {
                "Nancy Davolio",
                "Andrew Fuller",
                "Janet leverling"
            };
        }
    }
}
