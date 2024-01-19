using MediatR;

namespace HR.LeaveManagement.Application.Features.Command.DeleteLeaveType;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
