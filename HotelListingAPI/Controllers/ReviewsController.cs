using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Models.Country;
using HotelListingAPI.Models.Hotel;
using HotelListingAPI.Repository;
using HotelListingAPICore.Contracts;
using HotelListingAPICore.Models.Reviews;
using HotelListingAPIData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<ReviewsController> _logger;
        private readonly UserManager<User> _userManager;

        public ReviewsController(IMapper mapper, IReviewRepository reviewRepository, ILogger<ReviewsController> logger, UserManager<User> userManager)
        {
            this._mapper = mapper;
            this._reviewRepository = reviewRepository;
            this._logger = logger;
            this._userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetReviewDetailDto>> GetReview(int id)
        {
            var review = await _reviewRepository.GetAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetReviewDetailDto>(review));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Review>> PostReview(CreateReviewDto createReview)
        {
            var review = await _reviewRepository.AddAsync<CreateReviewDto, GetReviewDto>(createReview);
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _reviewRepository.GetUser(userEmail);
            return CreatedAtAction(nameof(GetReview), new { id = review.Id, user = currentUser }, review);
        }
    }
}
