namespace HouseService.Models
{
    using System.Collections.Generic;
    public class Location
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class LandBankHome
    {
        public string acquisition_date { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string city_council_district { get; set; }
        public string county { get; set; }
        public string inventory_type { get; set; }
        public string latitude { get; set; }
        public Location location_1 { get; set; }
        public string location_1_address { get; set; }
        public string location_1_city { get; set; }
        public string location_1_state { get; set; }
        public string longitude { get; set; }
        public string market_value { get; set; }
        public string market_value_year { get; set; }
        public string neighborhood { get; set; }
        public string off_street_parking { get; set; }
        public string parcel_number { get; set; }
        public string postal_code { get; set; }
        public string property_class { get; set; }
        public string property_condition { get; set; }
        public string property_status { get; set; }
        public string school_district { get; set; }
        public string square_footage { get; set; }
        public string state { get; set; }
        public string zoned_as { get; set; }
    }
}