using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
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
            var book = _bookStoreDbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadi");
            
            var vm = _mapper.Map<BookDetailView>(book);
            //     Genre = ((GenreEnum)book.GenreId).ToString(),
            //     PageCount = book.PageCount,
            //     Id = book.Id,
            //     PublishDate = book.PublishDate.ToString("dd/MM/yyyy"),
            //     Title = book.Title
            // };

            return vm;
        }
    }
        

    public class BookDetailView 
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }

}
