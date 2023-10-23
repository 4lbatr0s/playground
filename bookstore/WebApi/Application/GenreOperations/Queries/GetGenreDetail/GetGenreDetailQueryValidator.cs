using FluentValidation;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;

namespace WebApi.Application.GenreOperations;
    public class GetGenreDetailsQueryValidator:AbstractValidator<GetGenreDetailsQuery>
    {
        public GetGenreDetailsQueryValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
