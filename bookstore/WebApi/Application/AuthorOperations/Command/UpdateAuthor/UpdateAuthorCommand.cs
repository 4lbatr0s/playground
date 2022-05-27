using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDBContext _dbContext;

        public UpdateAuthorCommand(IBookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AuthorUpdateModel? Model;
        public int AuthorId;
        

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(b => b.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Kitap bulunamadi");
            
            author.Name = Model.Name != default ? Model.Name : author.Name; 
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
             
            _dbContext.SaveChanges();        
    }
        

    }

    public class AuthorUpdateModel
    {
        public string? Name { get; set; }
        public string? Surname {get; set;}
    }
 
}