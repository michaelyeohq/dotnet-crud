using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Users;
using Store.Dtos.Users;
using Store.Helpers;
using Store.Models;
using BCryptNet = BCrypt.Net.BCrypt;


namespace Store.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        public UserController(IUserRepo repository, IMapper mapper, IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _repository = repository;

        }

        //POST api/users/authenticate
        [HttpPost("authenticate")]
        public ActionResult AuthenticateUser(UserAuthenticateDto userAuthenticateDto)
        {
            var userName = userAuthenticateDto.Username;
            var password = userAuthenticateDto.Password;
            var user = _repository.AuthenticateUser(userName, password);

            var token = _jwtUtils.GenerateToken(user);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "login"
            });
        }

        //GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtUtils.ValidateToken(jwt);
                var users = _repository.GetAllUsers();

                return Ok(users);
            }
            catch (Exception)
            {
                throw new AppException("Unauthorized");
            }
        }

        //POST api/users
        [HttpPost]
        public ActionResult<User> CreateUser(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            // hash password
            user.PasswordHash = BCryptNet.HashPassword(userCreateDto.Password);
            _repository.CreateUser(user);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}