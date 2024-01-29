using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using HR.LeaveManagement.Domain;
using NSubstitute;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidRequest_ShouldCreateLeaveType()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var leaveTypeRepository = Substitute.For<ILeaveTypeRepository>();
            var handler = new CreateLeaveTypeCommandHandler(mapper, leaveTypeRepository);

            var command = new CreateLeaveTypeCommand
            {
                Name = "Vacation",
                DefaultDays = 10
                // Include other properties as needed
            };

            var validationResult = new FluentValidation.Results.ValidationResult();
            var validator = Substitute.For<IValidator<CreateLeaveTypeCommand>>();
            validator.ValidateAsync(Arg.Any<CreateLeaveTypeCommand>()).Returns(validationResult);
            var validators = new List<IValidator<CreateLeaveTypeCommand>> { validator };

            var leaveType = new LeaveType { Id = 1, Name = "Vacation", DefaultDays = 10 };
            mapper.Map<LeaveType>(Arg.Any<CreateLeaveTypeCommand>()).Returns(leaveType);
            leaveTypeRepository.IsLeaveTypeUnique(Arg.Any<string>()).Returns(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await leaveTypeRepository.Received(1).CreateAsync(Arg.Any<LeaveType>());
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Handle_WithInvalidRequest_ShouldThrowBadRequestException()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var leaveTypeRepository = Substitute.For<ILeaveTypeRepository>();
            var handler = new CreateLeaveTypeCommandHandler(mapper, leaveTypeRepository);

            var command = new CreateLeaveTypeCommand();

            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("PropertyName", "Error message"));

            var validator = Substitute.For<IValidator<CreateLeaveTypeCommand>>();
            validator.ValidateAsync(Arg.Any<CreateLeaveTypeCommand>()).Returns(validationResult);
            var validators = new List<IValidator<CreateLeaveTypeCommand>> { validator };

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
            await leaveTypeRepository.DidNotReceive().CreateAsync(Arg.Any<LeaveType>());
        }
    }
}
