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
                      await this.httpClient.GetAsync(new Uri("http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&regionId=18795&childtype=neighborhood"));
                    
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

        [HttpGet("getcomps")]
        public async Task<ActionResult> GetComps()
        {
          var response =
            await this.httpClient.GetAsync(new Uri("http://www.zillow.com/webservice/GetDeepComps.htm?zws-id=X1-ZWz197buben8jv_2ghte&zpid=58612516&count=5"));

          var responseString = await response.Content.ReadAsStringAsync();
          zillowResult = XDocument.Parse(responseString);
          // todo not the right comps object... yet
          var result = from c in zillowResult.Descendants("comp")
                    select new 
                    {
                      id = (string)c.Element("zpid"),
                      latitude = (string)c.Element("latitude"),
                      longitude = (string)c.Element("longitude"),
                      name = (string)c.Element("name"),
                      url = (string)c.Element("homedetails")
                    };
          return this.Ok(result.ToList());
        }
    }
}
