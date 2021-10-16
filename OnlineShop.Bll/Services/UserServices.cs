using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Common.Dtos.Account;
using OnlineShop.Common.Models;
using OnlineShop.Dal;
using OnlineShop.Domain.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; 

namespace OnlineShop.Bll.Services
{
    public class UserServices : IUserServices
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;

        public UserServices(UserManager<User> userManager, SignInManager<User> singInManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(UserRegisterDto dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("Register Model is null");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };
            }

            

            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
            };


            var result = await _userManager.CreateAsync(user, dto.Password);

            if(result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }
            

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(UserLoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            var checkPassword = await _singInManager.PasswordSignInAsync(user, dto.Password, false, false);
            var roles = await _userManager.GetRolesAsync(user);

            if (checkPassword.Succeeded)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthOptions:SecretKey"]));

                var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _configuration["AuthOptions:Issuer"],
                    audience: _configuration["AuthOptions:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                return new UserManagerResponse
                {
                    Message = encodedToken,
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "Wrong Credentials !",
                IsSuccess = false
            };
        }
    }
}
