using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Helpers;

namespace Restaurant_Angular.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IPasswordHasher<ApplicationUserDto> _passwordHasher;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly UserManager<ApplicationUserDto> _userManager;

        public AuthController(
            ILogger<AuthController> logger,
            IPasswordHasher<ApplicationUserDto> passwordHasher,
            IConfigurationRoot configurationRoot,
            UserManager<ApplicationUserDto> userManager
            )
        {
            _logger = logger;
            _passwordHasher = passwordHasher;
            _configurationRoot = configurationRoot;
            _userManager = userManager;
        }
        [HttpPost("Token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModelDto credentialModelDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(credentialModelDto.UserName);
                if (user != null)
                {
                    if (_passwordHasher.VerifyHashedPassword(user,user.Password, credentialModelDto.Password) == PasswordVerificationResult.Success)
                    {
                        return Ok(CreateToken(user));
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"JWT yaratırken bir hata oluştu: {ex.Message.ToString()}");
                
            }
            return null;
        }

        /// <summary>
        /// Create Token Private Methods.
        /// </summary>
        /// <param name="applicationUserDto"></param>
        /// <returns></returns>
        private async Task<JwtTokenPacket> CreateToken(ApplicationUserDto applicationUserDto)
        {

            var userClaims = await _userManager.GetClaimsAsync(applicationUserDto);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUserDto.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["Token:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _configurationRoot["Token:Issuer"], audience: _configurationRoot["Token:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: cred);

            return new JwtTokenPacket
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiretion = token.ValidFrom.ToString(),
                UserName = applicationUserDto.UserName,
            };
        }
    }
}
