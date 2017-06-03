namespace HouseService.Models
{
    using System.Collections.Generic;
    public class NeighborhoodResponse
    {
      public string code { get; set; }
      public List<HomeData> data { get; set; }
      public string message { get; set; }
      public string status { get; set; }
    }

    public class HomeData
  {
      public int? address_id { get; set; }
      public string census_block_2010_id { get; set; }
      public string census_block_2010_name { get; set; }
      public string census_county_id { get; set; }
      public string census_county_state_id { get; set; }
      public string census_latitude { get; set; }
      public string census_longitude { get; set; }
      public string census_metro_area { get; set; }
      public string census_tiger_line_id { get; set; }
      public string census_track_id { get; set; }
      public string census_track_name { get; set; }
      public string census_zip { get; set; }
      public string city { get; set; }
      public string city_classification { get; set; }
      public string city_council_district { get; set; }
      public int? city_id { get; set; }
      public int? city_land_bank_property { get; set; }
      public string city_land_use { get; set; }
      public string city_land_use_code { get; set; }
      public string city_neighborhood_census { get; set; }
      public string city_nhood { get; set; }
      public string city_nighborhood { get; set; }
      public object city_police_division { get; set; }
      public string city_sub_class { get; set; }
      public object city_tif { get; set; }
      public int? city_vacant_parcel { get; set; }
      public string county_assessed_improvement { get; set; }
      public string county_assessed_land { get; set; }
      public string county_assessed_value { get; set; }
      public object county_book_number { get; set; }
      public object county_cid { get; set; }
      public object county_common_area { get; set; }
      public object county_complex_name { get; set; }
      public object county_conveyance_area { get; set; }
      public object county_conveyance_designator { get; set; }
      public string county_delinquent_tax_2010 { get; set; }
      public string county_delinquent_tax_2011 { get; set; }
      public string county_delinquent_tax_2012 { get; set; }
      public string county_delinquent_tax_2013 { get; set; }
      public string county_delinquent_tax_2014 { get; set; }
      public string county_delinquent_tax_2015 { get; set; }
      public string county_document_number { get; set; }
      public string county_eff_from_date { get; set; }
      public string county_eff_to_date { get; set; }
      public string county_exempt { get; set; }
      public string county_extract_date { get; set; }
      public object county_floor_designator { get; set; }
      public object county_floor_name_designator { get; set; }
      public string county_id { get; set; }
      public string county_land_use_code { get; set; }
      public object county_legal_description { get; set; }
      public string county_market_value { get; set; }
      public object county_mtg_co { get; set; }
      public object county_mtg_co_address { get; set; }
      public object county_mtg_co_city { get; set; }
      public object county_mtg_co_state { get; set; }
      public object county_mtg_co_zip { get; set; }
      public string county_name { get; set; }
      public string county_neighborhood_code { get; set; }
      public string county_object_id { get; set; }
      public string county_owner { get; set; }
      public string county_owner_address { get; set; }
      public string county_owner_city { get; set; }
      public string county_owner_state { get; set; }
      public string county_owner_zip { get; set; }
      public object county_page_number { get; set; }
      public string county_parcel_number { get; set; }
      public string county_pca_code { get; set; }
      public string county_property_area { get; set; }
      public string county_property_picture { get; set; }
      public string county_property_report { get; set; }
      public string county_shape_st_area { get; set; }
      public string county_shape_st_area_1 { get; set; }
      public string county_shape_st_area_2 { get; set; }
      public string county_shape_st_legnth_2 { get; set; }
      public string county_shape_st_lenght { get; set; }
      public string county_shape_st_length_1 { get; set; }
      public object county_sim_con_div_type { get; set; }
      public string county_situs_address { get; set; }
      public string county_situs_city { get; set; }
      public string county_situs_state { get; set; }
      public string county_situs_zip { get; set; }
      public string county_stated_area { get; set; }
      public string county_tax_year { get; set; }
      public string county_taxable_value { get; set; }
      public string county_tca_code { get; set; }
      public object county_tif_district { get; set; }
      public object county_tif_project { get; set; }
      public string county_tot_sqf_l_area { get; set; }
      public string county_type { get; set; }
      public string county_year_built { get; set; }
      public object county_z_designator { get; set; }
      public string latitude { get; set; }
      public string longitude { get; set; }
      public string single_line_address { get; set; }
      public string state { get; set; }
      public string street_address { get; set; }
      public string zip { get; set; }
    }
}
