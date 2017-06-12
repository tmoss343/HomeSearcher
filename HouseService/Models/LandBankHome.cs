namespace HouseService.Models
{
    using System.Collections.Generic;
    
    public class Location
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class LandBankResult
    {
        public string acquisition_date { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string city_council_district { get; set; }
        public string inventory_type { get; set; }
        public string location_1_address { get; set; }
        public string location_1_city { get; set; }
        public string location_1_state { get; set; }
        public string market_value { get; set; }
        public string market_value_year { get; set; }
        public string neighborhood { get; set; }
        public string off_street_parking { get; set; }
        public string postal_code { get; set; }
        public string property_class { get; set; }
        public string property_condition { get; set; }
        public string property_status { get; set; }
        public string school_district { get; set; }
        public string square_footage { get; set; }
        public string zoned_as { get; set; }
    }

    public class LandBankHome
    {
        public LandBankResult ToResult()
        {
            return new LandBankResult
            {
                 acquisition_date = this.acquisition_date,
                 address = this.address,
                 city = this.city,
                 city_council_district = this.city_council_district,
                 inventory_type = this.inventory_type,
                 location_1_address = this.location_1_address,
                 location_1_city = this.location_1_city,
                 location_1_state = this.location_1_state,
                 market_value = this.market_value,
                 market_value_year = this.market_value_year,
                 neighborhood = this.neighborhood,
                 off_street_parking = this.off_street_parking,
                 postal_code = this.postal_code,
                 property_class = this.property_class,
                 property_condition = this.property_condition,
                 property_status = this.property_status,
                 school_district = this.school_district,
                 square_footage = this.square_footage,
                 zoned_as = this.zoned_as
            };
        }

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