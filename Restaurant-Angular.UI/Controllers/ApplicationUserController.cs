using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Angular.Business.Constants;
using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Data.DbModels;

namespace Restaurant_Angular.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserBusiness _applicationUserBusiness;

        public ApplicationUserController(IApplicationUserBusiness applicationUserBusiness)
        {
            _applicationUserBusiness = applicationUserBusiness;
        }

        [HttpGet("index")]
        public IActionResult Index( )
        {

            return Ok();
        }

        [HttpPost]
        [Route("register")]
        public async Task<Object> PostApllication(ApplicationUserDto applicationUserDto)
        {
            var data = _applicationUserBusiness.CreateAppUser(applicationUserDto);
            var result = data.Result.Data;
            return Ok(result);
        }



    }
}
