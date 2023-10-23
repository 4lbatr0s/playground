using FluentValidation;
using WebApi.Application.AuthorOperations;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4);
            RuleFor(command => command.Model.Surname).MinimumLength(4);
        }
    }
