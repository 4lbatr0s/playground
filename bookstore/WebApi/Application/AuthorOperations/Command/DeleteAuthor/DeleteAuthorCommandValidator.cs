
using FluentValidation;

namespace WebApi.Application.authorOperations.Commands.DeleteAuthor;


public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.authorId).GreaterThan(0);
    }
}
