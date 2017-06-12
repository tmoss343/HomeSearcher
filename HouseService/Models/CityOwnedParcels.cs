namespace HouseService.Models 
{    
    using System.Collections.Generic;

    public class PropertyLocation
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class CityOwnedParcels 
    {
        public string apn { get; set; }
        public string land_use_code { get; set; }
        public string pin { get; set; }
        public string property_address { get; set; }
        public string property_latitude { get; set; }
        public PropertyLocation property_location { get; set; }
        public string property_longitude { get; set; }
        public string property_owner_address { get; set; }
        public string property_owner_city { get; set; }
        public string property_owner_name { get; set; }
        public string property_owner_state { get; set; }
        public string property_owner_zipcode { get; set; }
        public string property_shape_area { get; set; }
        public string property_shape_len { get; set; }
    }
}