using BookingAPI.Interfaces;
using BookingAPI.Models;
using BookingAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationAction _reservationAction;
        public ReservationController(IReservationAction reservationAction)
        {
            _reservationAction = reservationAction;
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BookReservation([FromBody] Reservation reservation)
        {
            var result = _reservationAction.BookReservation(reservation);
            if (result == null)
            {
                return BadRequest(new { message = "Room is not available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditReservation([FromBody] Reservation reservation)
        {
            var result = _reservationAction.EditReservation(reservation);
            if (result == null)
            {
                return BadRequest(new { message = "Reservation is not available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CancelReservation([FromBody] Reservation reservation)
        {
            var result = _reservationAction.CancelReservation(reservation);
            if (result == null)
            {
                return BadRequest(new { message = "Reservation is not available" });
            }
            return Ok(result);
        }

        [Authorize(Roles ="staff")]
        [HttpPost]
        [ProducesResponseType(typeof(List<RoomDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllBookedRoom([FromBody] HotelDTO hotelDTO)
        {
            var result = _reservationAction.GetAllBookedRoom(hotelDTO);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "Hotel is not available" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Reservation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllReservation([FromBody] UserDTO userDTO)
        {
            var result = _reservationAction.GetAllReservation(userDTO);
            if (result.Count == 0)
            {
                return BadRequest(new { message = "User is not available" });
            }
            return Ok(result);
        }
    }
}
