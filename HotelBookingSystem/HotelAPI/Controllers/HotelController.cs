using HotelAPI.Exceptions;
using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllHotels()
        {
            try
            {
                var result = _hotelAction.GetAllHotels();
                if (result.Count == 0)
                {
                    return NotFound(new Error { errorNumber = 404, errorMessage = "No hotels available" });
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

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult AddHotel([FromBody] Hotel hotel)
        {
            try
            {
                if (hotel.HotelId != 0)
                {
                    return ValidationProblem(title: "Hotel Validation error occured", detail: "HotelId should be empty");
                }
                var result = _hotelAction.AddHotel(hotel);
                if (result == null)
                {
                    return BadRequest(new Error { errorNumber = 400, errorMessage = "Hotel already exists" });
                }
                return Created("Hotel", result);
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

        [Authorize(Roles = "staff")]
        [HttpPut]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]

        public IActionResult EditHotel([FromBody] Hotel hotel)
        {
            try
            {
                if (hotel.HotelId <= 0)
                {
                    return ValidationProblem(title: "Hotel Validation error occured", detail: "HotelId should be positive");
                }
                var result = _hotelAction.UpdateHotel(hotel);
                if (result == null)
                {
                    return NotFound(new { message = "Hotel is not available" });
                }
                return Ok(result);
            }
            catch(HotelException he)
            {
                return BadRequest(new Error { errorNumber = 400, errorMessage = he.Message });
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

        [Authorize(Roles = "staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]

        public IActionResult DeleteHotel([FromBody] HotelDTO hotelDTO)
        {
            try
            {
                if (hotelDTO.Id <= 0)
                {
                    return ValidationProblem(title: "Hotel Validation error occured", detail: "HotelId should be positive");
                }
                var result = _hotelAction.DeleteHotel(hotelDTO);
                if (result == null)
                {
                    return NotFound(new { message = "Hotel is not available" });
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

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ProducesResponseType(typeof(Room), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult AddRoom([FromBody] Room room)
        {
            try
            {
                if (room.RoomId != 0)
                {
                    return ValidationProblem(title: "Room validation error occured", detail: "RoomId should be empty");
                }
                var result = _hotelAction.AddRoom(room);
                if (result == null)
                {
                    return BadRequest(new { message = "Hotel not exists" });
                }
                return Created("Room", result);
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

        [Authorize(Roles = "staff")]
        [HttpPut]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]

        public IActionResult EditRoom([FromBody] Room room)
        {
            try
            {
                if (room.RoomId <= 0)
                {
                    return ValidationProblem(title: "Room validation error occured", detail: "RoomId should be positive");
                }
                var result = _hotelAction.UpdateRoom(room);
                if (result == null)
                {
                    return NotFound(new { message = "Room not Exist" });
                }
                return Ok(result);
            }
            catch(HotelException he)
            {
                return BadRequest(new Error { errorNumber = 400, errorMessage = he.Message });
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

        [Authorize(Roles = "staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteRoom([FromBody] RoomDTO roomDTO)
        {
            try
            {
                if (roomDTO.Id <= 0)
                {
                    return ValidationProblem(title: "Room validation error occured", detail: "RoomId should be positive");
                }
                var result = _hotelAction.DeleteRoom(roomDTO);
                if (result == null)
                {
                    return NotFound(new { message = "Room not Exist" });
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

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ProducesResponseType(typeof(Amenity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult AddAmenities([FromBody] Amenity amenities)
        {
            try
            {
                if (amenities.AmentityId != 0)
                {
                    return ValidationProblem(title: "Amenities validation error occured", detail: "AmenityId should be empty");
                }
                var result = _hotelAction.AddAmenities(amenities);
                if (result == null)
                {
                    return BadRequest(new { message = "Amenities already exists" });
                }
                return Created("Amenities",result);
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

        [Authorize(Roles = "staff")]
        [HttpPut]
        [ProducesResponseType(typeof(Amenity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult EditAmenities([FromBody] Amenity amenities)
        {
            try
            {
                if (amenities.AmentityId <= 0)
                {
                    return ValidationProblem(title: "Amenities validation error occured", detail: "AmenityId should be positive");
                }
                var result = _hotelAction.UpdateAmenities(amenities);
                if (result == null)
                {
                    return NotFound(new { message = "Amenities not Exist" });
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

        [Authorize(Roles = "staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(Amenity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<Amenity> DeleteAmentities([FromBody] AmenityDTO amenityDTO)
        {
            try
            {
                if (amenityDTO.Id <= 0)
                {
                    return ValidationProblem(title: "Amenities validation error occured", detail: "AmenityId should be positive");
                }
                var amentiy = _hotelAction.DeleteAmenities(amenityDTO);
                if (amentiy != null)
                {
                    return Ok(amentiy);
                }
                return NotFound(new Error { errorNumber = 400, errorMessage = "Amentities not exists" });
            }
            catch (AmentitiesException ae)
            {
                Debug.WriteLine(ae.StackTrace);
                return BadRequest(new Error { errorNumber = 400, errorMessage = ae.Message });
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

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ProducesResponseType(typeof(HotelAmenity), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<Amenity> AddAmentitiestoHotel([FromBody] HotelAmenity hotelAmenities)
        {
            try
            {
                if (hotelAmenities.HotelAmentityId != 0)
                {
                    return ValidationProblem(title: "HotelAmenities validation error occured", detail: "HotelAmenityId should be empty");
                }
                var amenity = _hotelAction.AddAmentitiesToHotel(hotelAmenities);
                if (amenity != null)
                {
                    return Created("Amentities", amenity);
                }
                return BadRequest(new Error { errorNumber = 400, errorMessage = "Amentities already to this hotel" });
            }
            catch(HotelException he)
            {
                return BadRequest(new Error { errorNumber = 400, errorMessage = he.Message });
            }
            catch (AmentitiesException ae)
            {
                return BadRequest(new Error { errorNumber = 400, errorMessage = ae.Message });
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

        [Authorize(Roles = "staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(HotelAmenity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelAmenity> RemoveAmentityFromHotel(HotelAmenityDTO hotelAmenityDTO)
        {
            try
            {
                if (hotelAmenityDTO.Id <= 0)
                {
                    return ValidationProblem(title: "HotelAmenities validation error occured", detail: "HotelAmenityId should be positive");
                }
                var amenity = _hotelAction.RemoveAmentitiesToHotel(hotelAmenityDTO);
                if (amenity != null)
                {
                    return Ok(amenity);
                }
                return NotFound(new Error { errorNumber = 400, errorMessage = "HotelAmentities not Exsits" });
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
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status404NotFound)]
        public ActionResult<List<Room>> GetAllRoomsByHotel([FromBody]  HotelDTO hotelDTO)
        {
            try
            {
                if (hotelDTO == null)
                {
                    return ValidationProblem(title: "Hotel validation error occured", detail: "Hotel shouldn't be empty");
                }
                if (hotelDTO.Id <= 0)
                {
                    return ValidationProblem(title: "Hotel validation error occured", detail: "HotelId should be positive");
                }
                var result = _hotelAction.GetAllRoomsByHotel(hotelDTO);
                if (result.Count == 0)
                {
                    return NotFound(new { message = "No rooms available" });
                }
                return Ok(result);
            }
            catch(HotelException he)
            {
                return BadRequest(new Error { errorNumber = 400, errorMessage = he.Message });
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
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Room>> GetRoomByHotel([FromBody] HotelRoomDTO hotelRoomDTO)
        {
            try
            {

                if (hotelRoomDTO == null)
                {
                    return ValidationProblem(title: "Hotel validation error occured", detail: "Hotel shouldn't be empty");
                }
                if (hotelRoomDTO.HotelId <= 0)
                {
                    return ValidationProblem(title: "Hotel validation error occured", detail: "HotelId should be positive");
                }
                if (hotelRoomDTO.RoomId <= 0)
                {
                    return ValidationProblem(title: "Room validation error occured", detail: "RoomId should be positive");
                }
                var result = _hotelAction.GetRoombyHotel(hotelRoomDTO);
                if (result == null)
                {
                    return NotFound(new { message = "No rooms available" });
                }
                return Ok(result);
            }
            catch(HotelException he)
            {
                return BadRequest(new Error { errorNumber = 0, errorMessage = he.Message });
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
        [ProducesResponseType(typeof(List<Amenity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Amenity>> GetAmenitiesByHotel([FromBody] HotelDTO hotelDTO)
        {
            try
            {
                var result = _hotelAction.GetAllAmenitiesbyHotel(hotelDTO);
                if (result.Count == 0)
                {
                    return NotFound(new { message = "No amenities available" });
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

        [HttpPost]
        [ProducesResponseType(typeof(List<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Hotel>> GetHotelFilters([FromBody] HotelFilterDTO hotelFilterDTO)
        {
            try
            {
                if (hotelFilterDTO.MaxPrice != null && hotelFilterDTO.MinPrice != null)
                {
                    if (hotelFilterDTO.MinPrice <= 0 || hotelFilterDTO.MaxPrice <= 0)
                    {
                        return ValidationProblem(title: "Validation Error Occured", detail: "Price can't be negative or zero "); ;
                    }
                    if (hotelFilterDTO.MinPrice > hotelFilterDTO.MaxPrice)
                    {
                        return ValidationProblem(title: "Validation Error Occured", detail: "MinPrice can't be greater than MaxPrice");
                    }
                }
                if (hotelFilterDTO.Country != null && hotelFilterDTO.Country.Length > 0)
                {
                    if (hotelFilterDTO.Country.Length > 50)
                    {
                        return ValidationProblem(title: "Validation Error Occured", detail: "Country name can't be greater than 50 characters");
                    }
                }
                if (hotelFilterDTO.City != null && hotelFilterDTO.City.Length > 0)
                {
                    if (hotelFilterDTO.City.Length > 50)
                    {
                        return ValidationProblem(title: "Validation Error Occured", detail: "City name can't be greater than 50 characters");
                    }
                }
                if (hotelFilterDTO.AmenityId <= 0)
                {
                    return ValidationProblem(title: "Validation Error Occured", detail: "AmenityId can't be negative or zero");
                }
                return Ok(_hotelAction.GetHotelbyFilter(hotelFilterDTO));
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
        [ProducesResponseType(typeof(List<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        public ActionResult<List<Room>> GetRoomByFilter([FromBody] RoomFilterDTO roomFilterDTO)
        {
            try
            {
                if (roomFilterDTO.HotelId <= 0)
                {
                    return ValidationProblem(title: "Validation Error Occured", detail: "HotelId can't be negative or zero");
                }
                if (roomFilterDTO.MaxPrice != null && roomFilterDTO.MinPrice != null)
                {
                    if (roomFilterDTO.MinPrice <= 0 || roomFilterDTO.MaxPrice <= 0)
                    {
                        return ValidationProblem(title: "Validation Error Occured", detail: "Price can't be negative or zero "); ;
                    }
                    if (roomFilterDTO.MinPrice > roomFilterDTO.MaxPrice)
                    {
                        return ValidationProblem(title: "Validation Error Occured", detail: "MinPrice can't be greater than MaxPrice");
                    }
                }
                if (roomFilterDTO.Capacity != null & roomFilterDTO.Capacity <= 0)
                {
                    return ValidationProblem(title: "Validation Error Occured", detail: "Capacity can't be negative or zero");
                }
                return Ok(_hotelAction.GetRoomsByFilter(roomFilterDTO));
            }
            catch(HotelException he)
            {
                return BadRequest(new Error { errorNumber = 400, errorMessage = he.Message });
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
        [ProducesResponseType(typeof(List<HotelCountDTO>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        public ActionResult<List<HotelCountDTO>> GetCountOfRoomAndAmenityForHotel([FromBody] HotelDTO hotelDTO)
        {
            try
            {
                var hotels = _hotelAction.GetRoomAndAmenityForHotel(hotelDTO);
                if (hotelDTO.Id <= 0)
                {
                    return ValidationProblem(title: "Validation Error Occured", detail: "HotelId can't be negative or zero");
                }
                if (hotels != null)
                {
                    return Ok(hotels);

                }
                return NotFound(new Error { errorNumber = 404, errorMessage = "hotel not Found" });
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

        [HttpGet]
        [ProducesResponseType(typeof(List<HotelCountDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<HotelCountDTO>> GetCountOfRoomAndAmenityForAllHotel()
        {
            try
            {
                var hotels = _hotelAction.GetRoomAndAmenityForAllHotel();
                if (hotels.Count > 0)
                {
                    return Ok(hotels);

                }
                return NotFound(new Error { errorNumber = 404, errorMessage = "no hotel  Found" });
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
