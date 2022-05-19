using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{

    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDBContext bookStoreDBContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDBContext;
            _mapper = mapper;
        }

        public BookDetailView Handle()
        {   
            var book = _bookStoreDbContext.Books.Include(x => x.Genre).Include(x=> x.Author).SingleOrDefault(b => b.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadi");
            
            var vm = _mapper.Map<BookDetailView>(book);

            return vm;
        }
    }
        

    public class BookDetailView 
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Genre { get; set; }
            public string? Author {get; set;}
            public int PageCount { get; set; }
            public string? PublishDate { get; set; }
        }

}
