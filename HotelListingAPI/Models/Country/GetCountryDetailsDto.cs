﻿using HotelListingAPI.Models.Hotel;

namespace HotelListingAPI.Models.Country
{
    public class GetCountryDetailsDto : BaseCountryDto
    {
        public int Id { get; set; }

        public List<GetHotel> Hotels { get; set; }

    }
}