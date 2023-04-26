using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPIData;
using HotelListingAPI.Models;

namespace HotelListingAPI.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
