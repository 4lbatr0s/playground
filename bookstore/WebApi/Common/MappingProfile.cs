using AutoMapper;
using WebApi.Application.AuthorOperations;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //source->target: CreateBookModel object should be mapped into Book.
            CreateMap<CreateBookModel, Book>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<CreateAuthorModel, Author>();
            //source->target: but modify target's genre.
            //to use Genre.Name, we need to Include Genre to _dbContext.Books inside of BookQueries..
            CreateMap<Book, BookDetailView>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<Book, BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailsView>();

            CreateMap<Author, AuthorsViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
            CreateMap<Author, AuthorDetailsView>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
        }
    }
