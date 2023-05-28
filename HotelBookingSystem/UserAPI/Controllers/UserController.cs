using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTO;

namespace UserAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAction _userAction;
        public UserController(IUserAction userAction)
        {
            _userAction = userAction;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> Login(UserDTO userDTO)
        {
            try
            {
                var user = _userAction.Login(userDTO);
                if (user == null)
                    return NotFound(new Error { errorNumber = 404, errorMessage = "Username or password is incorrect" });
                return Ok(user);
            }
            catch (SqlException se)
            {
                Debug.WriteLine(se.StackTrace);
                return BadRequest(new Error { errorNumber = 0, errorMessage = "Server is not working properly " });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return BadRequest(new Error { errorNumber = 0, errorMessage = "Something Went Wrong" });
            }
        }




        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> Register(UserRegisterDTO userDTO)
        {
            try
            {
                var user = _userAction.Register(userDTO);
                if (user == null)
                    return BadRequest(new Error { errorNumber = 400, errorMessage = "Username is already taken" });
                return Ok(user);
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
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdatePassword(UserUpdateDTO userDTO)
        {
            try
            {
                var user = _userAction.UpdatePassword(userDTO);
                if (user == null)
                    return BadRequest(new Error { errorNumber = 404, errorMessage = "Username is not found" });
                return Ok(user);
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
