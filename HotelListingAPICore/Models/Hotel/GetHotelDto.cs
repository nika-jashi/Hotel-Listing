using HotelListingAPICore.Models;

namespace HotelListingAPI.Models.Hotel
{
    public class GetHotelDto : BaseHotelDto, IBaseDto
    {
        public int Id { get; set; }

    }
}
