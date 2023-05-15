using HotelListingAPI.Contracts;
using HotelListingAPIData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPICore.Contracts
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<User> GetUser(string email);
    }
}
