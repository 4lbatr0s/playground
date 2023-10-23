using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.authorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDBContext _dbContext;

        public int authorId;
        public DeleteAuthorCommand(IBookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(b => b.Id == authorId);
            var book = _dbContext.Books.SingleOrDefault(b => b.AuthorId == authorId);

            if(author is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            
            else if(book is not null)
                throw new InvalidOperationException("Kitabi yayinda olan yazar silinemez");
 
             _dbContext.Remove(author);
             _dbContext.SaveChanges();
            
        }
    }


}