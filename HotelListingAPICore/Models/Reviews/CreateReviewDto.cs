using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPICore.Models.Reviews
{
    public class CreateReviewDto: BaseReviewDto
    {
        public int HotelId { get; set; }
    }
}
