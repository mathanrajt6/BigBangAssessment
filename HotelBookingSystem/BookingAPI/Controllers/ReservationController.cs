using BookingAPI.Interfaces;
using BookingAPI.Models;
using BookingAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

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
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        public IActionResult BookReservation([FromBody] Reservation reservation)
        {
            try
            {
                if (reservation.Id != 0)
                {
                    return ValidationProblem("Id should not be provided");
                }
                var result = _reservationAction.BookReservation(reservation);
                if (result == null)
                {
                    return BadRequest(new Error { errorNumber = 400, errorMessage = "Room is not available" });
                }
                return Created("Reservation", result);
            }
            catch(SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Something Went Wrong" });
            }
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]

        public IActionResult EditReservation([FromBody] Reservation reservation)
        {
            try
            {
                if (reservation.Id <= 0)
                {
                    return ValidationProblem("Id should be Positive");
                }
                var result = _reservationAction.EditReservation(reservation);
                if (result == null)
                {
                    return NotFound(new Error { errorNumber = 404, errorMessage = "Room is not available" });
                }
                return Ok(result);
            }
            catch (SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Something Went Wrong" });
            }
        }

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]

        public IActionResult CancelReservation([FromBody] ReservationDTO reservationDTO)
        {
            try
            {
                if (reservationDTO.Id <= 0)
                {
                    return ValidationProblem("Id should Positive");
                }
                var result = _reservationAction.CancelReservation(reservationDTO);
                if (result == null)
                {
                    return NotFound(new Error { errorNumber = 404, errorMessage = "Reservation is not available" });
                }
                return Ok(result);
            }
            catch (SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Something Went Wrong" });
            }
        }

        [Authorize(Roles ="staff")]
        [HttpPost]
        [ProducesResponseType(typeof(List<RoomDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]

        public IActionResult GetAllBookedRoom([FromBody] HotelDTO hotelDTO)
        {
            try
            {
                if (hotelDTO.Id <= 0)
                {
                    return ValidationProblem(title: "Hotel validation Error Occured", detail: "Hotel Id should be Positive");
                }
                var result = _reservationAction.GetAllBookedRoom(hotelDTO);
                if (result.Count == 0)
                {
                    return NotFound(new Error { errorNumber = 404, errorMessage = "Hotel is not available" });
                }
                return Ok(result);
            }
            catch (SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Something Went Wrong" });
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<Reservation>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult GetAllReservation([FromBody] UserDTO userDTO)
        {
            try
            {
                var result = _reservationAction.GetAllReservation(userDTO);
                if (result.Count == 0)
                {
                    return NotFound(new Error { errorNumber = 404, errorMessage = "User is not available" });
                }
                return Ok(result);
            }
            catch (SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Something Went Wrong" });
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(HotelCountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<HotelCountDTO> GetCountOfBookedRoomForHotel([FromBody] HotelDTO hotelDTO)
        {
            try
            {
                if (hotelDTO.Id <= 0)
                {
                    return ValidationProblem(title: "Hotel validation Error Occured", detail: "Hotel Id should be Positive");
                }
                var result = _reservationAction.GetCountOfBookedRoomForHotel(hotelDTO);
                if (result == null)
                {
                    return NotFound(new Error { errorNumber = 404, errorMessage = "No Booking for the Hotel" });
                }
                return Ok(result);
            }
            catch (SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Something Went Wrong" });
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(List<HotelCountDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public ActionResult<HotelCountDTO> GetCountOfBookedRoomForAllHotel([FromBody] DateDTO dateDTO)
        {
            try
            {
                var result = _reservationAction.GetCountOfBookedRoomForAllHotel(dateDTO);
                if (result == null)
                {
                    return NotFound(new Error { errorNumber = 404, errorMessage = "No Hotel Booking" });
                }
                return Ok(result);
            }
            catch (SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Something Went Wrong" });
            }
        }
    }
}
