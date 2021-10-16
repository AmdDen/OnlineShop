using OnlineShop.Common.Dtos.Account;
using OnlineShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Bll.Services
{
    public interface IUserServices
    {
        Task<UserManagerResponse> RegisterUserAsync(UserRegisterDto dto);

        Task<UserManagerResponse> LoginUserAsync(UserLoginDto dto);

        
    }
}
