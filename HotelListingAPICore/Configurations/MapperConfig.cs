using AutoMapper;
using HotelListingAPI.Models.Country;
using HotelListingAPI.Models.Hotel;
using HotelListingAPI.Models.User;
using HotelListingAPICore.Models.Country;
using HotelListingAPICore.Models.Reviews;
using HotelListingAPIData;

namespace HotelListingAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            //Country Mappings
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDetailsDto>().ReverseMap();

            //Hotel Mappings
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, GetHotelDto>().ReverseMap();
            CreateMap<Hotel, GetHotelDetailsDto>().ReverseMap();

            //User Mappings
            CreateMap<UserDto, User>().ReverseMap();

            //Review Mappings
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewDetailDto>().ReverseMap();
            CreateMap<Review, GetReviewDto>().ReverseMap();
        }
    }
}
