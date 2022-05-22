using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.Auth;

public interface ISignUpCommand
{
    Task<CommandResultResponse<string>> Execute(SignUpRequest request);
}