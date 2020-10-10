using System;
using System.Threading.Tasks;
using CityBuilder.Models.InputModels.IdentityInputModels;
using CityBuilder.Models.OutputModels.IdentityOutputModel;
using CityBuilder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityBuilder.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        public IdentityController(IdentityService identityService)
        {
            this.identityService = identityService;
        }

        private readonly IdentityService identityService;

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginInputModel loginInputModel)
        {
            var token = await this.identityService.Login(loginInputModel);
            return Ok(token);
        }
    }
}
