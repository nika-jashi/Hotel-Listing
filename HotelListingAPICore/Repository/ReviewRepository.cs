using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Repository;
using HotelListingAPICore.Contracts;
using HotelListingAPIData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPICore.Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<User> GetUser(string email)
        {
            var user = _context.Users.ToList().Where(u => u.Email == email).First();
            return user;
        }
    }
}
