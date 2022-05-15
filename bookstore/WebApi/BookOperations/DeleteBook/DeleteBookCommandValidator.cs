using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.Model.Id).GreaterThan(0);
        }
    }
}