using System;
using System.Threading.Tasks;
using Auga.BL.Authorize;
using Auga.BL.Models.AuthorizeDto;
using Auga.BL.ProfileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auga.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IProfileService _profileService;

        public UsersController(ITokenService tokenService, IProfileService profileService)
        {
            _tokenService = tokenService;
            _profileService = profileService;
        }

        /// <summary>
        /// Register user
        /// </summary>
        [HttpPost("user")]
        public async Task<IActionResult> Register(
            RegisterDto model
        )
        {
            try
            {
                var responsePayload = await _tokenService.RegisterAsync(model);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost("user/token")]
        public async Task<IActionResult> Token(
            LoginDto model
        )
        {
            try
            {
                var responsePayload = await _tokenService.LoginAsync(model);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        /// <summary>
        /// Retrieves a specific user by unique id
        /// </summary>
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> Profile(
            string userNickname
        )
        {
            try
            {
                var user = await _profileService.GetUserProfileInfo(userNickname);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        //[Authorize]
        //[HttpGet("settings")]
        //public async Task<IActionResult> Settings(
        //)
        //{
        //    try
        //    {
        //        var user = await _profileService.GetUserProfileInfo();
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { ex.Message });
        //    }
        //}

        //[Authorize]
        //[HttpPut("changePassword")]
        //public async Task<IActionResult> ChangePassword(ChangePasswordDto model
        //)
        //{
        //    try
        //    {
        //        await _profileService.ChangePasswordAsync(model);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { ex.Message });
        //    }
        //}

        //[Authorize]
        //[HttpPut("editProfile")]
        //public async Task<IActionResult> EditProfile(EditProfileDto model
        //)
        //{
        //    try
        //    {
        //        var user = await _profileService.ChangeProfileAsync(model);
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { ex.Message });
        //    }
        //}


        //[Authorize]
        //[HttpPost("is_token_valid")]
        //public OkResult IsTokenValid(
        //    LoginDto model
        //)
        //{
        //    return Ok();
        //}
    }
}