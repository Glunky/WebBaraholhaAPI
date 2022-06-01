using Microsoft.AspNetCore.Identity;
using WebBaraholkaAPI.Mappers.Auth.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Mappers.Auth.Implementations;

public class SignUpToRequestIdentityUserMapper : ISignUpToRequestIdentityUserMapper
{
    public DbApplicationUser Map(SignUpRequest request)
    {
        return new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.UserInfo.Login,
        };
    }
}