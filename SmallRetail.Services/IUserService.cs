using System;
using System.Collections.Generic;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public interface IUserService : IService<User>
    {
        bool Login(string username, string password);
    }
}