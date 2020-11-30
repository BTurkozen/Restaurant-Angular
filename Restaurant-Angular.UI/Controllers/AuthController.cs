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
using Restaurant_Angular.Data.DbModels;

namespace Restaurant_Angular.UI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            ILogger<AuthController> logger,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IConfigurationRoot configurationRoot,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _logger = logger;
            _passwordHasher = passwordHasher;
            _configurationRoot = configurationRoot;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CredentialModelDto credentialModelDto)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(credentialModelDto.UserName, credentialModelDto.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(credentialModelDto.UserName);
                    if (user != null)
                    {
                        var tokenPacket = CreateToken(user);
                        if (tokenPacket != null && tokenPacket.Result.Token != null)
                        {
                            return Ok(tokenPacket);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Logging yapılırken hata oluştu: {ex}");
            }
            return BadRequest("Login Başarılı olmadı. Lütfen bilgilerinizi tekrar kontrol ediniz.");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto registerModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parametreler hatalı");
            }
            try
            {
                var user = await _userManager.FindByNameAsync(registerModelDto.UserName);
                if (user != null)
                {
                    return BadRequest("Bu kullanıcı zaten mevcut");
                }
                else
                {
                    user = new ApplicationUser
                    {
                        FirstName = registerModelDto.FirstName,
                        LastName = registerModelDto.LastName,
                        UserName = registerModelDto.UserName,
                        Email = registerModelDto.Email
                    };
                    var result = await _userManager.CreateAsync(user, registerModelDto.Password);
                    if (result.Succeeded)
                    {
                        return Ok(CreateToken(user));
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Kayıt esnasında exception hatası alındı:  {ex}");
                return BadRequest($"Yeni Kullanıcı Kaydı Esnasında  Hata Alındı: {ex}");
            }
        }



        [HttpPost("Token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModelDto credentialModelDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(credentialModelDto.UserName);
                if (user != null)
                {
                    if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, credentialModelDto.Password) == PasswordVerificationResult.Success)
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
        /// <param name="appUser"></param>
        /// <returns></returns>
        private async Task<JwtTokenPacket> CreateToken(ApplicationUser appUser)
        {

            var userClaims = await _userManager.GetClaimsAsync(appUser);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
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
                UserName = appUser.UserName,
            };
        }
    }
}
