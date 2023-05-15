using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPIData
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        [Required]
        [Range(1.0, 10.0)]
        public double Rating { get; set; }
        public User User { get; set; }
        
        [ForeignKey(nameof(HotelId))]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
