using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.CreateUser.Commands;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class UserController:ControllerBase //mvc uzayindan gelir.
    {
        private readonly IBookStoreDBContext _bookStoreDbContext; // readonly : cannot be changed inside from application.
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration; //appsettings.json altındaki verilere ulasmamı saglar. 
        public UserController(IBookStoreDBContext bookStoreDbContext, IMapper mapper, IConfiguration configuration)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
            _configuration = configuration;
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_bookStoreDbContext, _mapper);
            command.Model = newUser;
            // CreateUserCommandValidator validator = new CreateUserCommandValidator();
            // validator.Validate(command);
            var user = command.Handle();

            return Ok(user);
        }

        [HttpPost("connect/token")] //isterse 3rd party servislerden token isteyelim, isterse kendi projemizde isteyelim connect/token bir standarttır.
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_bookStoreDbContext, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }

        //refresh token ile yeniden accesstoken üretimi.
        [HttpGet("refreshToken")] 
        public ActionResult<Token> Refresh([FromBody] string token) //buradaki token postmanda soru işaretiyle verilir /users/refreshToken?token=.....
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_bookStoreDbContext, _mapper, _configuration);
            command.RefreshToken = token;
            var result = command.Handle();
            return result;
        }


    }
}