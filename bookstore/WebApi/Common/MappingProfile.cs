using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.GetBooks.GetBookDetailQuery;

namespace WebApi.Common
{

    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //source->target: CreateBookModel object should be mapped into Book.
            CreateMap<CreateBookModel, Book>();
            //source->target: but modify target's genre.
            CreateMap<Book, BookDetailView>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));

        }
    }
}