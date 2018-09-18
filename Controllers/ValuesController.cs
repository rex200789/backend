using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
  
        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

/* 
        // GET api/values/5
        [HttpGet("{id}")]
        [EnableCors("CrossOriginPolicy")]
        public ActionResult<string> Get(string id)
        {
            
            var client = _httpClientFactory.CreateClient();
            var result = client.GetStringAsync("http://api.walmartlabs.com/v1/nbp?apiKey=86uydk66yy93v2bmytuazcvw&format=json&itemId=" + id);
            return Ok(result.ToString());
        }
*/
        // GET api/values/5
        [HttpGet("{id}")]
        [EnableCors("CrossOriginPolicy")]
        public async Task<IActionResult>  Get(string id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.walmartlabs.com/");
                    var response = await client.GetAsync("v1/nbp?apiKey=86uydk66yy93v2bmytuazcvw&format=json&itemId=" + id);
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();                    
                    return Ok(stringResult);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting results from api: {httpRequestException.Message}");
                }
            }
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
