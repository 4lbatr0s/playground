using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.CreateBook.Commands
{
    public class CreateBookCommand
    {
        private readonly IBookStoreDBContext? _dbContext;
        private readonly IMapper? _mapper;
        public CreateBookModel? Model;
        public CreateBookCommand(IBookStoreDBContext? dbContext, IMapper? mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Title == Model.Title);
            if(book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            
            //Map model into book object.
            //target -> book.
            book = _mapper.Map<Book>(Model);
            //{
            //     GenreId = Model.GenreId,
            //     PageCount = Model.PageCount,
            //     PublishDate = Model.PublishDate,
            //     Title = Model.Title
            // };


            _dbContext.Add(book);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string? Title {get; set;}
        public int GenreId {get; set;}
        public int PageCount {get; set;}
        public int AuthorId {get; set;}
        public DateTime PublishDate { get; set; }
    }

}