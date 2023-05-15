using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPICore.Models.Reviews
{
    public class BaseReviewDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        
        [Range(1.0, 10.0)]
        public double Rating { get; set; }
    }
}
