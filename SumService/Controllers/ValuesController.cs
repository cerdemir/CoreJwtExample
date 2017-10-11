using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace SumService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return new ObjectResult(
                new {
                    data=new string[] { "value1", "value2","Ivir","Zivir" },
                    user = User.Identity.Name
            });
        }
    }
}
