using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{

    public class GetBooksQuery
    {
        private readonly IBookStoreDBContext _bookStoreDbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookStoreDBContext bookStoreDBContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDBContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            //d => d.Genre: join books and genre
            var bookList = _bookStoreDbContext.Books.Include(d => d.Genre).Include(d=> d.Author).OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);//Should be List<BooksViewModel>
            
            // foreach (var book in bookList)
            // {
            //     vm.Add(new BooksViewModel
            //     {
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreId).ToString(), //Do not forget to convert to string, because Genre is string in viewModel.
            //         Id = book.Id,
            //         PageCount = book.PageCount,
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
            //     });
            // }
            return vm;

        }
    }


    public class BooksViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Author {get; set;}
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
    }

}
