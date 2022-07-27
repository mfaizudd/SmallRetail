using System;
using System.Collections.Generic;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public interface IUserService : IService<User>
    {
        User? Login(string username, string password);
    }
}