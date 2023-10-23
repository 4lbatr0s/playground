using FluentValidation;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(command => command.Model.isActive).NotNull();
            RuleFor(command => command.Model.Name).MinimumLength(4).NotEmpty();
        }
    }
