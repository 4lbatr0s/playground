using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public DeleteBookModel Model;
        public DeleteBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == Model.Id);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            _dbContext.Remove(book);
            _dbContext.SaveChanges();
        }
    }


    public class DeleteBookModel
    {
        public int Id { get; set; }
    }

}