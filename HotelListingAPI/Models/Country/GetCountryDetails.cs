using HotelListingAPI.Models.Hotel;

namespace HotelListingAPI.Models.Country
{
    public class GetCountryDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public List<GetHotel> Hotels { get; set; }

    }
}
