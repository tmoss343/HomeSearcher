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

namespace HouseService.Controllers
{
    public class HouseHomeDetails
    {
      public string id { get; set; }
      public string lastsoldprice { get; set; }
      public string lastsolddate { get; set; }
      public string lotSizeSqFt { get; set; }
      public string finishedSqFt { get; set; }
      public string bathrooms { get; set; }
      public string bedrooms { get; set; }
      public string zestimate { get; set; }
      public string lastUpdated { get; set; }
      public string address { get; set; }
      public string url { get; set; }
    }

    [EnableCors("AllowSpecificOrigin")]
    [Route("houses")]
    public class HouseController : Controller
    {
        private HttpClient httpClient;

        private string[] Neighborhoods = new string[]
        {
          "Waldo%20Homes",
          "West%20Plaza",
          "Pendleton%20Heights",
          "Beacon%20Hills",
          "Union%20Hill"
        };

        XDocument HouseResult;

        public HouseController()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&state=mo&city=kansascity&childtype=neighborhood");
        }
        // GET api/values
        [HttpGet("kcregions")]
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
                    HouseResult = XDocument.Parse(responseString);
                    result = from c in HouseResult.Descendants("region")
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

        [HttpGet("comps")]
        public async Task<ActionResult> GetComps()
        {
          
      var response =
            await this.httpClient.GetAsync(new Uri("http://www.zillow.com/webservice/GetDeepComps.htm?zws-id=X1-ZWz197buben8jv_2ghte&zpid=58612516&count=5"));

          var responseString = await response.Content.ReadAsStringAsync();
          HouseResult = XDocument.Parse(responseString);
          // todo not the right comps object... yet
          var result = from c in HouseResult.Descendants("comp")
                    select new 
                    {
                      id = (string)c.Element("zpid"),
                      latitude = (string)c.Element("address").Element("latitude").Value,
                      longitude = (string)c.Element("address").Element("longitude").Value,
                      name = ((string)c.Element("address").Element("street").Value) +" "+((string)c.Element("address").Element("zipcode").Value),
                      url = (string)c.Element("links").Element("homedetails")
                    };
          return this.Ok(result.ToList());
        }

        [HttpGet("propertysearchresults")]
        public async Task<ActionResult> GetSearchResults()
        {
          List<HouseHomeDetails> houseInfo = new List<HouseHomeDetails>();
          foreach (var hood in Neighborhoods)
          {

            var addressResult = await this.httpClient.GetAsync(new Uri("http://dev-api.codeforkc.org/address-by-neighborhood/V0/"+hood+"?city=Kansas%20City&state=MO"));
            var neighborhoods = await addressResult.Content.ReadAsStringAsync();

            var addressResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<NeighborhoodResponse>(neighborhoods);

            var goodHouses = addressResponse.data.Where<HomeData>(c => (c.city_land_use != "Commercial") && c.county_market_value != null && (Int32.Parse(c.county_market_value) >= 80000));

            
            foreach (var house in goodHouses)
            {
              house.street_address = house.street_address.Replace("ST", "");
              house.street_address = house.street_address.Replace("TER", "");
              house.street_address = house.street_address.Replace("AVE", "");
              house.street_address = house.street_address.Replace("BLVD", "");
              house.street_address = house.street_address.Replace("CR", "");
              house.street_address = house.street_address.Replace("LN", "");
              house.street_address = house.street_address.Replace("PL", "");
              house.street_address = house.street_address.Replace("PLZ", "");
              house.street_address = house.street_address.Replace("RD", "");
              house.street_address = house.street_address.Replace("HWY", "");
              var response =
                await this.httpClient.GetAsync(new Uri("http://www.zillow.com/webservice/GetDeepSearchResults.htm?zws-id=X1-ZWz197buben8jv_2ghte&address=" + house.street_address.Replace(" ", "") + "&citystatezip=" + house.city.Replace(" ", "") + "%2C+" + house.state));

              var responseString = await response.Content.ReadAsStringAsync();
              HouseResult = XDocument.Parse(responseString);
              // todo not the right comps object... yet
              var results = from c in HouseResult.Descendants("result")
                            select new HouseHomeDetails
                            {
                              id = (string)c.Element("zpid"),
                              lastsoldprice = (string)c.Element("lastSoldPrice"),
                              lastsolddate = (string)c.Element("lastSoldDate"),
                              finishedSqFt = (string)c.Element("finishedSqFt"),
                              bathrooms = (string)c.Element("bathrooms"),
                              bedrooms = (string)c.Element("bedrooms"),
                              zestimate = (string)c.Element("zestimate").Element("amount"),
                              lastUpdated = (string)c.Element("zestimate").Element("last-updated"),
                              address = ((string)c.Element("address").Element("street")) + " " + ((string)c.Element("address").Element("zipcode")),
                              url = (string)c.Element("links").Element("homedetails")
                            };
              foreach (var result in results)
              {
                houseInfo.Add(result);
              }
            }
          }
          return this.Ok(houseInfo.ToList());
        } 
    }
}