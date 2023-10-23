using AutoMapper;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries;

public class GetAuthorDetailsQueryValidator:AbstractValidator<GetAuthorDetailsQuery>
{
   public GetAuthorDetailsQueryValidator()
   {
       RuleFor(command => command.AuthorId).GreaterThanOrEqualTo(1);
   }
}
