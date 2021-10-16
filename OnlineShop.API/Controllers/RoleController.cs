using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Auth;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Common.Dtos.Role;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    public class RoleController: AppBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                var result = await _roleManager.CreateAsync(new Role{Name = name});

                if(result.Succeeded)
                {
                    return Ok(result);
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }

            return BadRequest("Please enter a role name");
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] RoleAddDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user != null)
            {
                bool roleExist = await _roleManager.RoleExistsAsync(dto.RoleName);
                if (roleExist)
                {
                    var result = await _userManager.AddToRoleAsync(user, dto.RoleName);

                    if (result.Succeeded)
                    {
                        return Ok($"User {user.UserName} granted role {dto.RoleName}");
                    }

                    return BadRequest("Error :: role not grandted");
                }
            }

            return BadRequest("User not found");
        }
    }
}