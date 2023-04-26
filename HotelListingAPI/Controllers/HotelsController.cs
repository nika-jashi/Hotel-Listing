using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPIData;
using HotelListingAPI.Contracts;
using AutoMapper;
using HotelListingAPI.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using HotelListingAPI.Models;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _hotelsRepository;

        public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
        {
            this._hotelsRepository = hotelsRepository;
            this._mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet("GetAll")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotel = await _hotelsRepository.GetAllAsync();
            return Ok(_mapper.Map<List<GetHotelDto>>(hotel));
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GetHotelDto>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            var pagedHotels = await _hotelsRepository.GetAllAsync<GetHotelDto>(queryParameters);
            return Ok(pagedHotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetHotelDto>(hotel));
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutHotel(int id, GetHotelDto updateHotel)
        {
            if (id != updateHotel.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            //_context.Entry(hotel).State = EntityState.Modified;
            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            _mapper.Map(updateHotel,hotel);

            try
            {
                await _hotelsRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotel)
        {
            var hotel = _mapper.Map<Hotel>(createHotel);
            await _hotelsRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
