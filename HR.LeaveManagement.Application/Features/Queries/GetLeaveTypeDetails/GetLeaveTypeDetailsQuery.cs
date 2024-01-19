using MediatR;

namespace HR.LeaveManagement.Application.Features.Queries.GetLeaveTypeDetails
{
    public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>; 
}
