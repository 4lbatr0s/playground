using FluentValidation;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.Application.BookOperations.Commands.DeleteBook;
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
