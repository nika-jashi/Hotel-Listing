using HotelListingAPI.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPICore.Models.Country
{
    public class UpdateCountryDto : BaseCountryDto, IBaseDto
    {
        public int Id { get; set; }
    }
}
