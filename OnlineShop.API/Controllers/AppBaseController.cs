using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.API.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class AppBaseController: ControllerBase
    {
        
    }
}