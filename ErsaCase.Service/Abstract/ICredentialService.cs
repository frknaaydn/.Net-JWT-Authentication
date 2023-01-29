using ErsaCase.Core;
using ErsaCase.Core.Dtos.UserDto;
using ErsaCase.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Service.Abstract
{
    public interface ICredentialService
    {
        Task<DataResult<UserResponseDto>> Authenticate(string userName, string password);
        Task<DataResult<User>> GetUserWithUserName(string userName);
        Task<DataResult<User>> Register(UserAddDto user);
        Task<DataResult<UserResponseDto>> UpdateUser(UserUpdateDto updateUserDto);
        int GetUserIdFromToken(string token);
        string PasswordToHash(string password);
        //Task<StaticResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto);
        //Task<StaticResult> UpdatePassword(UpdatePasswordDto updatePasswordDto);


        Task<DataResult<UserResponseDto>> GetCurrentUser();
        //StaticResult<ActiveUserDto> GetActiveUser();
        //DataResult<ActiveUserDto> GetActiveUserCompanyId();
    }
}
