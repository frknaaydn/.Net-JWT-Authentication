using ErsaCase.Core;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace ErsaCase.Service.CustomHelper
{
    public class CustomHelper
    {

        public StaticResult<ClaimsPrincipal> GetClaims(IHttpContextAccessor httpContextAccessor)
        {
            throw new Exception();
            //try
            //{
            //    //(Claim id, Claim name, Claim logicalRef)
            //    if (httpContextAccessor.HttpContext != null)
            //    {
            //        var userClaims = httpContextAccessor.HttpContext.User;

            //        if (userClaims != null)
            //        {
            //            //return StaticResult<ClaimsPrincipal>(HttpStatusCode.OK,userClaims);
            //            return StaticResult<ClaimsPrincipal>(HttpStatusCode.OK, userClaims);
            //        }

            //        return StaticResult<ClaimsPrincipal>(HttpStatusCode.NotFound,"Claims Not Found");
            //    }

            //    return StaticResult<ClaimsPrincipal>(HttpStatusCode.NotFound, "Claims Not Found");
            //}
            //catch (Exception ex)
            //{
            //    return StaticResult<ClaimsPrincipal>(HttpStatusCode.InternalServerError,ex.Message);
            //}
        }
    }
}
