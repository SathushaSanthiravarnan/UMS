using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        User GetUserByNIC(string nic); 
        List<User> GetAllUsers();
       
    }
}
