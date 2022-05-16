using System.Threading.Tasks;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Business.Commands.Interfaces;

public interface ISignInCommand
{
    Task<CommandResultResponse<bool>> Execute(SignInRequest request);
}