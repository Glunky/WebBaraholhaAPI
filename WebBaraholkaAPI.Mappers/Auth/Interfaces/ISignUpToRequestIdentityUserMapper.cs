using Microsoft.AspNetCore.Identity;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Mappers.Auth.Interfaces;

public interface ISignUpToRequestIdentityUserMapper
{
    IdentityUser Map(SignUpRequest request);
}