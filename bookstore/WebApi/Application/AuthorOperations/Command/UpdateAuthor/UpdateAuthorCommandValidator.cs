using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4);
            // RuleFor(command => command.Model.PageCount).GreaterThan(0);
            // RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            // RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Surname).MinimumLength(4);
        }
    }
}