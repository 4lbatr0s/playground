using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDBContext _dbContext;

        public UpdateBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookUpdateModel Model;
        public int BookId;


        

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadi");
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbContext.SaveChanges();        
    }
        

    }

    public class BookUpdateModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}