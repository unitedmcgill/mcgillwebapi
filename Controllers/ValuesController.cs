using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using McGill.Web;

namespace McGillWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // Get api/values/x/y
        [HttpGet("{x}/{y}")]
        [AllowAnonymous]
        public string Get(int x, int y)
        {
            //return Convert.ToString(x+y);
            return Convert.ToString(DoMathThings.AddThreeNumbers(x, y, 7));
        }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [AllowAnonymous]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [AllowAnonymous]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public void Delete(int id)
        {
        }
    }
}
