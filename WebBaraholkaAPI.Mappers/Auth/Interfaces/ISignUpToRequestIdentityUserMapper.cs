using Microsoft.AspNetCore.Identity;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Mappers.Auth.Interfaces;

public interface ISignUpToRequestIdentityUserMapper
{
    DbApplicationUser Map(SignUpRequest request);
}