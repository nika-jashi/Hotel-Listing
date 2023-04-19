using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Contracts
{
    public interface IAuthManager
    {
            Task<IEnumerable<IdentityError>> Register(UserDto userDto);
            Task<bool> Login(LoginUserDto loginUserDto);
    }
}
