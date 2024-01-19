using MediatR;

namespace HR.LeaveManagement.Application.Features.Queries.GetAllLeaveTypes;

/*public class GetLeaveTypeQuery : IRequest<List<LeaveTypeDto>>
{
}*/
public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
