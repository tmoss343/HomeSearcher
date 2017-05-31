using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft;

namespace ZillowService.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class ZillowController : Controller
    {
        private HttpClient httpClient;

        XDocument zillowResult;

        public ZillowController()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&state=mo&city=kansascity&childtype=neighborhood");
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<Region> result;
            using (httpClient)
            {
                try
                {
                    var response =
                      await this.httpClient.GetAsync(new Uri("http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&state=mo&city=kansascity&childtype=neighborhood"));
                    
                    var responseString = await response.Content.ReadAsStringAsync();
                    zillowResult = XDocument.Parse(responseString);
                    result = from c in zillowResult.Descendants("region")
                                                 select new Region()
                                                 {
                                                   id = (string)c.Element("id"),
                                                   latitude = (string)c.Element("latitude"),
                                                   longitude = (string)c.Element("longitude"),
                                                   name = (string)c.Element("name"),
                                                   url = (string)c.Element("url")
                                                 };
                }
                catch (Exception e)
                {
                    throw;
                }
                
            }
            return this.Ok(result.ToList<Region>());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
