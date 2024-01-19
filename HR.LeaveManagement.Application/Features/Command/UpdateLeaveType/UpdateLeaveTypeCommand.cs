using MediatR;

namespace HR.LeaveManagement.Application.Features.Command.UpdateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
