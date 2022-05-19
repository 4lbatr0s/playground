using FluentValidation;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.Application.BookOperations.Commands.CreateBook;
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);        
        }
    }
