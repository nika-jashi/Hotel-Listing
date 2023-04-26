using HotelListingAPI.Contracts;
using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<UserController> _logger;

        public UserController(IAuthManager authManager, ILogger<UserController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }
        //api/User/registration
        [HttpPost]
        [Route("registration")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");
            try
            {
                var errors = await _authManager.Register(userDto);
                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)} - User Registration attempt for {userDto.Email}");
                return Problem($"Something Went Wrong In The {nameof(Register)}", statusCode: 500);
            }
            
        }
        //api/User/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            _logger.LogInformation($"Login Attempt for {loginUserDto.Email}");
            try
            {
                var authResponse = await _authManager.Login(loginUserDto);
                
                if (authResponse == null)
                {
                    return Unauthorized();
                }

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)} - User Login attempt for {loginUserDto.Email}");
                return Problem($"Something Went Wrong In The {nameof(Login)}", statusCode: 500);
            }

        }
        //api/User/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            try
            {
                var authResponse = await _authManager.VerifyRefreshToken(request);

                if (authResponse == null)
                {
                    return Unauthorized();
                }

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(RefreshToken)}");
                return Problem($"Something Went Wrong In The {nameof(RefreshToken)}", statusCode: 500);
            }

        }
    }
}
