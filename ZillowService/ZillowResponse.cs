using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZillowService
{
  public class Message
  {
    public string code { get; set; }
    public string text { get; set; }
  }

  public class Request
  {
    public string childtype { get; set; }
    public string city { get; set; }
    public string state { get; set; }
  }

  public class Region
  {
    public string id { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string name { get; set; }
    public string url { get; set; }
  }

  public class List
  {
    public string count { get; set; }
    public List<Region> region { get; set; }
  }

  public class Region2
  {
    public string id { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
  }

  public class Response
  {
    public List list { get; set; }
    public Region2 region { get; set; }
    public string subregiontype { get; set; }
  }

  public class RegionChildrenRegionchildren
  { 
    public string regionChildren { get; set; }
    public string xsi { get; set; }
    public string schemaLocation { get; set; }
    public Message message { get; set; }
    public Request request { get; set; }
    public Response response { get; set; }
  }

  public class RootObject
  {
    public RegionChildrenRegionchildren regionChildren { get; set; }
  }
}
