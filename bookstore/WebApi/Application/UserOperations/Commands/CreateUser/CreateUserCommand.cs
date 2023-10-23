using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.CreateUser.Commands
{
    public class CreateUserCommand
    {
        private readonly IBookStoreDBContext? _dbContext;
        private readonly IMapper? _mapper;
        public CreateUserModel? Model;
        public CreateUserCommand(IBookStoreDBContext? dbContext, IMapper? mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public User Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);
            if(user is not null)
                throw new InvalidOperationException("Bu emaile sahip bir üye zaten mevcut");
            
            //Map model into user object.
            //target  -> source.
            //create user by model.
            user = _mapper.Map<User>(Model);
            //{
            //     GenreId = Model.GenreId,
            //     PageCount = Model.PageCount,
            //     PublishDate = Model.PublishDate,
            //     Title = Model.Title
            // };


            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return user;

        }
    }

    public class CreateUserModel
    {
        public string? Name {get; set;}
        public string Surname {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        //refresh token propları acccess tokendan sonra üretilecek.
     }

}