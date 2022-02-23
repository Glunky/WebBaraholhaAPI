using WebBaraholkaAPI.Models.Dto.Models.Consumers;

namespace WebBaraholkaAPI.Models.Dto.Requests.Auth;

public class SignUpRequest
{
    public ConsumerInformation ConsumerInformation { get; set; }
}