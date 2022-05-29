using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken;

    public class RefreshTokenCommand
    {
        private readonly IBookStoreDBContext? _dbContext;
        private readonly IMapper? _mapper;
        private readonly IConfiguration _configuration;
        public string RefreshToken;
        public RefreshTokenCommand(IBookStoreDBContext? dbContext, IMapper? mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }


        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate>DateTime.Now );
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
                throw new InvalidOperationException("Refresh token bulunamadı!");
            }
            //Return null


        }
    }

    

