using HotelListingAPI.Models.Hotel;
using HotelListingAPICore.Models;

namespace HotelListingAPI.Models.Country
{
    public class GetCountryDto : BaseCountryDto, IBaseDto
    {
        public int Id { get; set; }
    }



}
