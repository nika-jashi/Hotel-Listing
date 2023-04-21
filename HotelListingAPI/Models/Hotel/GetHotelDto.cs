namespace HotelListingAPI.Models.Hotel
{
    public class GetHotelDto : BaseHotelDto
    {
        public int Id { get; set; }

        public int CountryId { get; set; }
    }
}
