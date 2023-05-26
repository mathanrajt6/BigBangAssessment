using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var user = _userAction.Login(userDTO);
            if (user == null)
                return NotFound(new { message = "Username or password is incorrect" });
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> Register(UserRegisterDTO userDTO)
        {
            var user = _userAction.Register(userDTO);
            if (user == null)
                return BadRequest(new { message = "Username is already taken" });
            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdatePassword(UserUpdateDTO userDTO)
        {
            var user = _userAction.UpdatePassword(userDTO);
            if (user == null)
                return BadRequest(new { message = "Username is not found" });
            return Ok(user);
        }
    }
}
