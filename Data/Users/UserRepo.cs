using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Store.Helpers;
using Store.Models;
using BCryptNet = BCrypt.Net.BCrypt;


namespace Store.Data.Users
{
    public class UserRepo : IUserRepo
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        
        public UserRepo(Context context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public User AuthenticateUser(string userName, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName);

            // validate
            if (user == null || !BCryptNet.Verify(password, user.PasswordHash))
            {
                throw new AppException("Username or password is incorrect");
            }
            
            // authentication successful
            var response = _mapper.Map<User>(user);
            // response.JwtToken = _jwtUtils.GenerateToken(user);
            return response;


        }

        public void CreateUser(User user)
        {
            // validate
            if (_context.Users.Any(x => x.UserName == user.UserName))
            {
                throw new AppException("Username '" + user.UserName + "' is already taken");
            }
            // save user
            _context.Users.Add(user);

        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}