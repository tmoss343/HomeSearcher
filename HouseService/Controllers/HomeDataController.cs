namespace HouseService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft;
    using HouseService.Models;

    [EnableCors("AllowSpecificOrigin")]
    [Route("houses")]
    public class HouseController : Controller
    {
        private HttpClient httpClient;

        private IMemoryCache memCache;

        private string[] Neighborhoods = new string[]
        {
          "Waldo%20Homes",
          "West%20Plaza",
          "Pendleton%20Heights",
          "Beacon%20Hills",
          "Union%20Hill"
        };

        XDocument HouseResult;

        public HouseController(IMemoryCache memoryCache)
        {
            this.memCache = memoryCache;
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&state=mo&city=kansascity&childtype=neighborhood");
        }
        // GET api/values
        [HttpGet("kcregions")]
        public async Task<ActionResult> Get()
        {
          var cacheHoodsInfo = new List<Region>();
          if (!this.memCache.TryGetValue("hoods", out cacheHoodsInfo))
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
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromDays(40));
                memCache.Set("hoods", result.ToList(), cacheEntryOptions);
                this.memCache.TryGetValue("hoods", out cacheHoodsInfo);
            }
          }
            return this.Ok(cacheHoodsInfo);
        }

        [HttpGet("comps")]
        public async Task<ActionResult> GetComps([FromQuery] string id)
        {

          var response =
            await this.httpClient.GetAsync(new Uri("http://www.zillow.com/webservice/GetDeepComps.htm?zws-id=X1-ZWz197buben8jv_2ghte&zpid="+id+"&count=5"));

          var responseString = await response.Content.ReadAsStringAsync();
          HouseResult = XDocument.Parse(responseString);
          // todo not the right comps object... yet
          var result = from c in HouseResult.Descendants("comp")
                   select new
                   {
                     id = (string)c.Element("zpid"),
                     latitude = (string)c.Element("address").Element("latitude").Value,
                     longitude = (string)c.Element("address").Element("longitude").Value,
                     name = ((string)c.Element("address").Element("street").Value) + " " + ((string)c.Element("address").Element("zipcode").Value),
                     url = (string)c.Element("links").Element("homedetails"),
                     neighborhood = (string)c.Element("localRealEstate").Element("region").Attribute("id").Value
                    };
          return this.Ok(result.ToList());
        }

        [HttpGet("propertysearchresults")]
        public async Task<ActionResult> GetSearchResults()
        {
          var cacheHouseInfo = new List<HouseHomeDetails>();
          if (!this.memCache.TryGetValue("houses", out cacheHouseInfo))
          {
            var houseInfo = new List<HouseHomeDetails>();
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
                                longitude = (string)c.Element("address").Element("longitude"),
                                latitude = (string)c.Element("address").Element("latitude"),
                                finishedSqFt = (string)c.Element("finishedSqFt"),
                                bathrooms = (string)c.Element("bathrooms"),
                                bedrooms = (string)c.Element("bedrooms"),
                                zestimate = (string)c.Element("zestimate").Element("amount"),
                                lastUpdated = (string)c.Element("zestimate").Element("last-updated"),
                                address = ((string)c.Element("address").Element("street")) + " " + ((string)c.Element("address").Element("zipcode")),
                                url = (string)c.Element("links").Element("homedetails"),
                                neighborhood = (string)c.Element("localRealEstate").Element("region").Attribute("id").Value
                              };
                foreach (var result in results)
                {
                  houseInfo.Add(result);
                }
              
              }
              // Set cache options.
              var cacheEntryOptions = new MemoryCacheEntryOptions()
                  // Keep in cache for this time, reset time if accessed.
                  .SetSlidingExpiration(TimeSpan.FromDays(5));
              memCache.Set("houses", houseInfo, cacheEntryOptions);
              this.memCache.TryGetValue("houses", out cacheHouseInfo);
            }
          }
          
          return this.Ok(cacheHouseInfo.ToList());
        }

        [HttpGet("landbankhouses")]
        public async Task<ActionResult> GetFromTheLandBank()
        {
          var addressResult = await this.httpClient.GetAsync(new Uri("https://data.kcmo.org/resource/n653-v74j.json"));
          var addresses = await addressResult.Content.ReadAsStringAsync();

          var addressResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LandBankHome>>(addresses);
          float outlong;
          float outlat;

          var whatIcareAbout = addressResponse.Where(address => ((float.TryParse(address.latitude, out outlat) && (float.Parse(address.latitude, CultureInfo.InvariantCulture.NumberFormat) >= 39.0229 || float.Parse(address.latitude, CultureInfo.InvariantCulture.NumberFormat) <= 39.0642)) &&
                                                          (float.TryParse(address.longitude, out outlong) && (float.Parse(address.longitude, CultureInfo.InvariantCulture.NumberFormat) >= -94.3703 || float.Parse(address.longitude, CultureInfo.InvariantCulture.NumberFormat) <= -94.3358)))
                                );
          List<LandBankResult> result = new List<LandBankResult>();
          foreach(var thing in whatIcareAbout)
          {
            result.Add(thing.ToResult());
          }
          return this.Ok(result);
        }

        [HttpGet("cityownedparcels")]
        public async Task<ActionResult> GetFromCityParcels()
        {
          var addressResult = await this.httpClient.GetAsync(new Uri("https://data.kcmo.org/resource/nvpt-tmm9.json"));
          var addresses = await addressResult.Content.ReadAsStringAsync();

          var addressResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CityOwnedParcels>>(addresses);

          return this.Ok(addressResponse.Where(address => ((float.Parse(address.property_latitude, CultureInfo.InvariantCulture.NumberFormat) >= 39.110 || float.Parse(address.property_latitude, CultureInfo.InvariantCulture.NumberFormat) <= 38.911) &&
                                                           (float.Parse(address.property_longitude, CultureInfo.InvariantCulture.NumberFormat) >= -94.3628 || float.Parse(address.property_longitude, CultureInfo.InvariantCulture.NumberFormat) <= -94.3401) &&
                                                           (address.land_use_code == "1111" || address.land_use_code == "1121" || address.land_use_code == "1122" || address.land_use_code == "1126" || address.land_use_code == "2300" || address.land_use_code == "1121" || address.land_use_code == "9500"))
                                              )
                        );
        }
    }
}
