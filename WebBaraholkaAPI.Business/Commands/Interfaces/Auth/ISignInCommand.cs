using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.Auth;

public interface ISignInCommand
{
    Task<CommandResultResponse<bool>> Execute(SignInRequest request);
}