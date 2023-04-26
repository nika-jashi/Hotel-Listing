using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Models.Hotel
{
    public class GetHotelDetailsDto : BaseHotelDto
    {
        public int Id { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
