using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.Models.Country;
using HotelListingAPI.Models.Hotel;

namespace HotelListingAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<Country, CreateCountry>().ReverseMap();
            CreateMap<Country, GetCountry>().ReverseMap();
            CreateMap<Country, GetCountryDetails>().ReverseMap();
            CreateMap<Hotel, GetHotel>().ReverseMap();
        }
    }
}
