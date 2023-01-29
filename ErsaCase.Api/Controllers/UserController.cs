using ErsaCase.Core.Dtos;
using ErsaCase.Core.Dtos.UserDto;
using ErsaCase.Core.Model;
using ErsaCase.Repository.Abstract;
using ErsaCase.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ErsaCase.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static User user = new User();
        //private readonly ICredentialService _credentialService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(/*ICredentialService credentialService*/ IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            //_credentialService = credentialService;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserLoginDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.UserName = request.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var userAdd = new User();
            userAdd.UserName = request.UserName;
            userAdd.PasswordHash = passwordHash;
            userAdd.PasswordSalt = passwordSalt;

            await _unitOfWork.User.AddAsync(userAdd);
            await _unitOfWork.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            //var result = await .GetCurrentUser();

            if (user.UserName != request.UserName)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        //{
        //    var result = await _credentialService.Authenticate(userLoginDto.UserName, userLoginDto.Password);
        //    return !result.Succeeded ? BadRequest(result.ErrorDefination) : Ok(result.Data);
        //}

        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateUser(UserAddDto createUser)
        //{
        //    if (createUser.Password != createUser.ConfirmPassword)
        //        return BadRequest("Password not match");

        //    var result = await _credentialService.Register(createUser);
        //    return !result.Succeeded ? BadRequest(result.ErrorDefination) : Ok(result.Data);
        //}

        //[HttpGet]
        //[Authorize("ActionUserPolicy")]
        //public async Task<IActionResult> GetUser()
        //{
        //    var result = await _credentialService.GetCurrentUser();

        //    return !result.Succeeded ? BadRequest(result.ErrorDefination) : Ok(result.Data);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserId(int id)
        //{
        //    var result = await _credentialService.GetCurrentUser();

        //    return !result.Succeeded ? BadRequest(result.ErrorDefination) : Ok(result.Data);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        //{
        //    //Log.Information("Trying To Update A User On Controller");
        //    var result = await _credentialService.UpdateUser(userUpdateDto);

        //    if (!result.Succeeded)
        //    {
        //        //Log.Information("A User Could Not Updated On Controller");
        //        return BadRequest(result.ErrorDefination);
        //    }

        //    //Log.Information("A User Updated On Controller", result.Data);
        //    return Ok(result.Data);
        //}


        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        //[AllowAnonymous]
        //[HttpPost("ForgetPassword")]
        //public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Mail is not valid");
        //    }

        //    var result = await _credentialService.ForgetPassword(forgetPasswordDto);

        //    if (result.Succeeded)
        //    {
        //        return ActionResultBuilder.OkResult();
        //    }

        //    return BadRequest(result.ErrorMessage);
        //}

        //[HttpPut("UpdatePassword")]
        //public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto)
        //{
        //    Log.Information("Trying To Update A User On Controller");
        //    var result = await _credentialService.UpdatePassword(updatePasswordDto);

        //    if (result.Succeeded)
        //    {
        //        //Log.Information("A User Could Not Updated On Controller");
        //        //return BadRequest(result.ErrorDefination);
        //        return ActionResultBuilder.OkResult();
        //    }

        //    //return BadRequest(result.Error);
        //    return ActionResultBuilder.FailResult(result.ErrorMessage);
        //}

    }
}
