using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDBContext _dbContext;

        public int BookId;
        public DeleteBookCommand(IBookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            _dbContext.Remove(book);
            _dbContext.SaveChanges();
        }
    }


}