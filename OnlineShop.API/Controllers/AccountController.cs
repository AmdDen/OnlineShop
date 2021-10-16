using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Bll.Services;
using OnlineShop.Common.Dtos.Account;
using OnlineShop.Common.Models;
using OnlineShop.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : AppBaseController
    {
        private readonly IUserServices _userServices;

        private readonly UserManager<User> _userManager;

        public AccountController(IUserServices userServices, UserManager<User> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }

        // /api/account/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.RegisterUserAsync(dto);

                if (result.IsSuccess)
                {
                    return Ok(result); // Status Code: 200
                }
                return BadRequest(result);
            }

            return BadRequest("some proprieties are not valid !"); //Status Code: 400
        }

        // /api/account/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto dto)
        {
            if(ModelState.IsValid)
            {
                var result = await _userServices.LoginUserAsync(dto);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
            }

            return Unauthorized();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("console")]
        public async Task<IActionResult> GetInfo()
        {
            var user = await _userManager.FindByEmailAsync("admin@admin.com");
            var result = await _userManager.GetRolesAsync(user);

            return Ok(result);
        }
    }
}
