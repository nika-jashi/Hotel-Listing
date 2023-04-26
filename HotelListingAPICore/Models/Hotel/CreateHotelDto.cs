using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Models.Hotel
{
    public class CreateHotelDto : BaseHotelDto
    {
        [Required]
        public int CountryId { get; set; }
    }
}
