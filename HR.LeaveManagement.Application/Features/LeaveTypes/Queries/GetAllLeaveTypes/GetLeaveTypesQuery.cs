using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;

/*public class GetLeaveTypeQuery : IRequest<List<LeaveTypeDto>>
{
}*/
public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
