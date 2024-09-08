using FluentValidation;

namespace ResourceManager.Application.Users.CreateUser;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty();
    }
}
