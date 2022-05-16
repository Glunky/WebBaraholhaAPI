using WebBaraholkaAPI.Models.Dto.Models.Users;

namespace WebBaraholkaAPI.Models.Dto.Requests.Auth;

public class SignInRequest
{
    public UserInfo UserInfo { get; set; }
}