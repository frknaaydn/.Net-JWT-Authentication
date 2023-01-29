using ErsaCase.Core;
using ErsaCase.Core.Dtos.UserDto;
using ErsaCase.Core.Model;
using ErsaCase.Repository.Abstract;
using ErsaCase.Service.Abstract;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Service.Concrete
{
    public class CredentialService : ICredentialService
    {
        private readonly string HashSecret = "u8x/A?D*G-KaPdSg";
        private readonly string JwtSecret = "$B&E(H+MbQeThWmZq4t7w!z%C*F-J@Nc";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomHelper.CustomHelper _customHelper;

        private int ActiveUserId;
        private string ActiveUserName;

        public CredentialService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, CustomHelper.CustomHelper customHelper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _customHelper = customHelper;

            var getActiveUser = this.GetActiveUser();

            if (getActiveUser.Succeeded)
            {
                var activeUser = getActiveUser.Data;

                this.ActiveUserId = activeUser == null ? 0 : activeUser.Id;
                this.ActiveUserName = activeUser == null ? "root" : activeUser.Name;
            }
        }

        public async Task<DataResult<UserResponseDto>> Authenticate(string userName, string password)
        {
            throw new Exception();
            //var findedUser = await GetUserWithUserName(userName);

            //if (!findedUser.Succeeded)
            //{
            //    return new DataResult<UserResponseDto>("LoginFail", "Username or password is incorrect");
            //}

            //if (findedUser.Data?.Password == PasswordToHash(password))
            //{
            //    //var userReposData = await _unitOfWork.User.GetAsync(x => x.Id == findedUser.Data.Id, x => x.UserCompany);
            //    var userModel = _unitOfWork.User.GetAllWithThenInclude().Where(x => x.Id == findedUser.Data.Id)
            //        .FirstOrDefault();

            //    var jwtGeneratedUser = GenerateJwtToken(findedUser.Data).Data;


            //    var userResponse = new UserResponseDto()
            //    {
            //        Id = jwtGeneratedUser.Id,
            //        Name = jwtGeneratedUser.Name,
            //        UserName = jwtGeneratedUser.UserName,
            //        Email = jwtGeneratedUser.Email,
            //        Address = jwtGeneratedUser.Address,
            //        Phone = jwtGeneratedUser.Phone,
            //        RoleName = jwtGeneratedUser.Role.Name,
            //        //RoleId = userRoleModel.RoleId,
            //        TokenRaw = jwtGeneratedUser.TokenRaw,

            //    };

            //    return new DataResult<UserResponseDto>(userResponse);
            //}
            //else
            //{
            //    var userReposData = await _unitOfWork.User.GetAsync(x => x.Id == findedUser.Data.Id);
            //    await _unitOfWork.User.UpdateAsync(userReposData);

            //    return new DataResult<UserResponseDto>("LoginFail", "Username or password is incorrect");
            //}
        }

        public async Task<DataResult<UserResponseDto>> GetCurrentUser()
        {
            throw new Exception();
            //try
            //{

            //    DateTime dateTime = DateTime.Now;
            //    var activeUser = this.GetActiveUser().Data;
            //    var activeUserID = activeUser.Id;
            //    //string fileName = $"{activeUser.Name}_{dateTime.FullDateAndTimeStringWithUnderscore()}";

            //    if (this.GetActiveUser()?.Data?.Id > 0)
            //    {
            //        var user = _unitOfWork.User.FindAll(x => x.Id == this.GetActiveUser().Data.Id).FirstOrDefault();
            //        //var companySettings = user.UserCompany.CompanySetting;
            //        //var mapUser = _mapper.Map<UserResponseDto>(user);

            //        //mapUser.CompanyId = user.UserCompany.Id;
            //        //mapUser.CompanyName = user.UserCompany.Name;
            //        //mapUser.Image = (await _imageHelper.GetBydIdImage(user.Image)).Data;


            //        return user != null ? new DataResult<UserResponseDto>(mapUser) : new DataResult<UserResponseDto>("", "User not found");
            //    }
            //    else
            //    {
            //        return new DataResult<UserResponseDto>(HttpStatusCode.NotFound, "fail");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return new DataResult<UserResponseDto>(HttpStatusCode.InternalServerError, ex.Message);
            //}
        }

        public int GetUserIdFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(JwtSecret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //var logicRef = Guid.Parse(jwtToken.Claims.First(x => x.Type == "logicalRef").Value);

                return userId;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<DataResult<User>> GetUserWithUserName(string userName)
        {
            try
            {
                if (!await _unitOfWork.User.AnyAsync(x => x.UserName == userName))
                {
                    return new DataResult<User>("UserNotFound", "User Not Found");
                }

                var user = await _unitOfWork.User.GetAsync(x => x.UserName == userName, x => x.Role);

                return new DataResult<User>(user);

            }
            catch (Exception ex)
            {
                return new DataResult<User>(false, null, "Username ile kullanıcı getirilemedi", ex.Message);
            }

        }

        public string PasswordToHash(string password)
        {

            byte[] salt = Encoding.ASCII.GetBytes(HashSecret);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            return hashed;
        }

        public Task<DataResult<User>> Register(UserAddDto user)
        {
            throw new NotImplementedException();
            //try
            //{
            //    //Log.Information("Registering user {@user}", createUser.Name);
            //    createUser.Password = PasswordToHash(createUser.Password);

            //    if (await _unitOfWork.User.AnyAsync(u => u.UserName == createUser.UserName))
            //    {
            //        //Log.Information("User {@user} already exists", createUser.Name);
            //        return new DataResult<User>("UserNameExists", "UserName already exists");
            //    }
            //    if (!await _unitOfWork.Company.AnyAsync(c => c.Id == createUser.CompanyId))
            //    {
            //        Log.Information("Company {@company} does not exist");
            //        return new DataResult<User>("CompanyNotExist", "User company does not exist");
            //    }

            //    var user = _mapper.Map<User>(createUser);

            //    //böyle bir compnay ve role bulunmama durumları ?
            //    user.Role = await _unitOfWork.Role.GetAsync(x => x.Name == "default");

            //    var userCompany = await _unitOfWork.Company.GetAsync(x => x.Id == createUser.CompanyId);


            //    user.UserCompany = userCompany;

            //    await _unitOfWork.User.AddAsync(user);
            //    await _unitOfWork.SaveChanges();

            //    return new DataResult<User>(GenerateJwtToken(user).Data);

            //}
            //catch (Exception ex)
            //{
            //    return new DataResult<User>(false, null, "Register işleminde hata", ex.Message);
            //}
        }

        public Task<DataResult<UserResponseDto>> UpdateUser(UserUpdateDto updateUserDto)
        {
            throw new NotImplementedException();
        }

        public DataResult<ActiveUserDto> GetActiveUser()
        {
            try
            {
                var getClaimsPrincipal = _customHelper.GetClaims(_httpContextAccessor);

                if (getClaimsPrincipal.Succeeded)
                {
                    var userClaims = getClaimsPrincipal.Data;

                    var id = userClaims.FindFirst(x => x.Type == "id");
                    var name = userClaims.FindFirst(x => x.Type == "name");
                    var logicalRef = userClaims.FindFirst(x => x.Type == "logicalRef");

                    ActiveUserDto activeUserDto = new()
                    {
                        Id = id == null ? 0 : Convert.ToInt32(id.Value),
                        LogicalRef = logicalRef == null ? Guid.Empty : Guid.Parse(logicalRef.Value),
                        Name = name == null ? "" : name.Value,
                    };

                    return new DataResult<ActiveUserDto>(HttpStatusCode.OK, activeUserDto);

                }
                return new DataResult<ActiveUserDto>(HttpStatusCode.NotFound, "fail");

            }
            catch (Exception ex)
            {
                return new DataResult<ActiveUserDto>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private DataResult<User> GenerateJwtToken(User user)
        {
            throw new Exception();
            //try
            //{
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var key = Encoding.ASCII.GetBytes(JwtSecret);
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //        {
            //        new Claim("id", user.Id.ToString()),
            //        new Claim("name", user.Name.ToString()),
            //        new Claim("logicalRef",user.LogicalRef.ToString()),

            //        }),
            //        Expires = DateTime.UtcNow.AddMinutes(360), // Token ne kadar sure gecerli oldugunu belirtilir.
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            //    };
            //    var token = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            //    user.TokenRaw = token.RawData;

            //    return new DataResult<User>(user);
            //}
            //catch (Exception ex)
            //{
            //    return new DataResult<User>(HttpStatusCode.InternalServerError, ex.Message);
            //}
        }


        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
