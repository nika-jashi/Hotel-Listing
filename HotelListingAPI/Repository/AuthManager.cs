using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AuthManager(IMapper mapper, UserManager<User> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<bool> Login(LoginUserDto loginUserDto)
        {
            bool isValidUser = false;
            
            try
            {
                var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
                isValidUser = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            }
            catch (Exception)
            {
            }
            return isValidUser;
            
        }

        public async Task<IEnumerable<IdentityError>> Register(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(user,userDto.Password);


            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            
            return result.Errors.ToList();


        }
    }
}
