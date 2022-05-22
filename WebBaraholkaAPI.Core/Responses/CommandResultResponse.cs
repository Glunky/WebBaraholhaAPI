using WebBaraholkaAPI.Core.Enums;

namespace WebBaraholkaAPI.Core.Responses;

public class CommandResultResponse<T>
{
    public T? Body { get; set; }
    public CommandResultStatus Status { get; set; }
    public List<string> Errors { get; set; } = new();
}