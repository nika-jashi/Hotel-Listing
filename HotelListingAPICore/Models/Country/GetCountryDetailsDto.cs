using HotelListingAPI.Models.Hotel;
using HotelListingAPICore.Models;

namespace HotelListingAPI.Models.Country
{
    public class GetCountryDetailsDto : BaseCountryDto, IBaseDto
    {
        public int Id { get; set; }

        public List<GetHotelDto> Hotels { get; set; }
    }
}
