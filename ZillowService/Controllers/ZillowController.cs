using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZillowService.Controllers
{
    [Route("api/[controller]")]
    public class ZillowController : Controller
    {
        private HttpClient httpClient;

        XPathDocument zillowResult;

        public ZillowController()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&state=mo&city=kansascity&childtype=neighborhood");
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            using (httpClient)
            {
                try
                {
                    var response =
                      await this.httpClient.GetAsync(new Uri("http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&state=mo&city=kansascity&childtype=neighborhood"));
                    
                    var responseString = await response.Content.ReadAsStringAsync();
                    
                    XmlSerializer serializer = new XmlSerializer(typeof(RootObject));
                    using (TextReader reader = new StringReader(responseString))
                    {
                      zillowResult = new XPathDocument(reader);
                    }

                    var nav = zillowResult.CreateNavigator();
                    var result = nav.Evaluate("RegionChildren");
                }
                catch (Exception e)
                {
                    throw;
                }
                
            }
            return new string[] { "value1", "value2" };
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
