using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelAction _hotelAction;
        public HotelController(IHotelAction hotelAction)
        {
            _hotelAction = hotelAction;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllHotels()
        {
            var result = _hotelAction.GetAllHotels();
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No hotels available" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddHotel([FromBody] Hotel hotel)
        {
            var result = _hotelAction.AddHotel(hotel);
            if (result == null)
            {
                return BadRequest(new { message = "Hotel already exists" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpPut]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public IActionResult EditHotel([FromBody] Hotel hotel)
        {
            var result = _hotelAction.UpdateHotel(hotel);
            if (result == null)
            {
                return BadRequest(new { message = "Hotel is not available" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteHotel([FromBody] Hotel hotel)
        {
            var result = _hotelAction.DeleteHotel(hotel);
            if (result == null)
            {
                return BadRequest(new { message = "Hotel is not available" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddRoom([FromBody] Room room)
        {
            var result = _hotelAction.AddRoom(room);
            if (result == null)
            {
                return BadRequest(new { message = "Hotel not exists" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpPut]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditRoom([FromBody] Room room)
        {
            var result = _hotelAction.UpdateRoom(room);
            if (result == null)
            {
                return BadRequest(new { message = "Room not Exist" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteRoom([FromBody] Room room)
        {
            var result = _hotelAction.DeleteRoom(room);
            if (result == null)
            {
                return BadRequest(new { message = "Room not Exist" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Room>> GetRoomByHotel([FromBody] Hotel hotel)
        {
            var result = _hotelAction.GetAllRoomsByHotel(hotel);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No rooms available" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ProducesResponseType(typeof(AmentitiesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AmentitiesDTO> AddAmenitiesToHotel(AmentitiesDTO amentitiesDTO)
        {
            var result = _hotelAction.AddAmenities(amentitiesDTO);
            if (result == null)
            {
                return BadRequest(new { message = "Hotel not exists" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpPut]
        [ProducesResponseType(typeof(Amenities), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Amenities> EditAmenities(AmentitiesDTO amentitiesDTO)
        {
            var result = _hotelAction.UpdateAmenities(amentitiesDTO);
            if (result == null)
            {
                return BadRequest(new { message = "Amenities not exists" });
            }
            return Ok(result);
        }

        [Authorize(Roles = "staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(AmentitiesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AmentitiesDTO> DeleteAmenities(AmentitiesDTO amentitiesDTO)
        {
            var result = _hotelAction.DeleteAmenities(amentitiesDTO);
            if (result == null)
            {
                return BadRequest(new { message = "Amenities not exists" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Amenities>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Amenities>> GetAmenitiesByHotel([FromBody] Hotel hotel)
        {
            var result = _hotelAction.GetAllAmenitiesbyHotel(hotel);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No amenities available" });
            }
            return Ok(result);
        }


        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Hotel>> GetHotelByAmentities([FromBody] Amenities amenities)
        {
            var result = _hotelAction.GetHotelByAmentities(amenities);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No Hotels available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Hotel>> GetHotelByPriceRange([FromBody] HotelFilterDTO hotelFilterDTO)
        {
            var result = _hotelAction.GetHotelByPriceRange(hotelFilterDTO);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No Hotels available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Hotel>> GetHotelByCountry([FromBody] HotelFilterDTO hotelFilterDTO)
        {
            var result = _hotelAction.GetHotelByCountry(hotelFilterDTO);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No Hotel available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Hotel>> GetHotelByCity([FromBody] HotelFilterDTO hotelFilterDTO)
        {
            var result = _hotelAction.GetHotelByCity(hotelFilterDTO);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No Hotel available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Room>> GetRoomsByAC([FromBody] Hotel hotel)
        {
            var result = _hotelAction.GetRoomsByAC(hotel);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No Rooms available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Room>> GetRoomsByCapacity([FromBody] RoomFilterDTO roomFilterDTO)
        {
            var result = _hotelAction.GetRoomsByCapacity(roomFilterDTO);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No Rooms available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Room>> GetRoomsByPriceRange([FromBody] RoomFilterDTO roomFilterDTO)
        {
            var result = _hotelAction.GetRoomsByPriceRange(roomFilterDTO);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "No Rooms available" });
            }
            return Ok(result);
        }



    }
}
