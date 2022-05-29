using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken;

    public class CreateTokenCommand
    {
        private readonly IBookStoreDBContext? _dbContext;
        private readonly IMapper? _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenModel? Model;
        public CreateTokenCommand(IBookStoreDBContext? dbContext, IMapper? mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }


        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null)
            {
                //Token yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);


                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5); //15 + 5 REFRESH tokeni anca 20 dk kullanabiliriz.
                _dbContext.SaveChanges();
                return token;
            } 

            else 
            {
                throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı");
            }
            //Return null


        }
    }

    public class CreateTokenModel
    {
        public string Email {get; set;}
        public string Password {get; set;}
        //refresh token propları acccess tokendan sonra üretilecek.
     }

