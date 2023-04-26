namespace HotelListingAPI.Models.Country
{
    public class CreateCountryDto : BaseCountryDto
    {

    }
    public class UpdateCountryDto : BaseCountryDto
    {
        public int Id { get; set; }
    }
}
