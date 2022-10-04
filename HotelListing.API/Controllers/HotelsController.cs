﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using AutoMapper;
using HotelListing.API.Models.Hotels;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IGenericRepositoryV2 _genericRepositoryV2;
        private readonly IMapper _mapper;

        public HotelsController(IMapper mapper, IGenericRepositoryV2 genericRepositoryV2)
        {
            this._mapper = mapper;
            this._genericRepositoryV2 = genericRepositoryV2;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _genericRepositoryV2.GetAllAsync<Hotel>();
            return Ok(_mapper.Map<List<HotelDto>>(hotels));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _genericRepositoryV2.GetByIdAsync<HotelDto>(id);


            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<HotelDto>>(hotel));
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }

            var hotel = await _genericRepositoryV2.GetByIdAsync<Hotel>(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(hotelDto, hotel);

            try
            {
                await _genericRepositoryV2.UpdateAsync(hotel);
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _genericRepositoryV2.AddAsync<Hotel>(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _genericRepositoryV2.GetByIdAsync<Hotel>(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _genericRepositoryV2.Remove<Hotel>(hotel);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            var hotel = await _genericRepositoryV2.GetByIdAsync<Hotel>(id);

            if (hotel == null)
            {
                return false;
            }
        return true;
        }
    }
}
