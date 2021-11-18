using System.Collections.Generic;
using Store.Dtos.Users;
using Store.Models;

namespace Store.Data.Users
{
    public interface IUserRepo
    {
        // Save db changes
        bool SaveChanges();
        User AuthenticateUser(string userName, string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}