using SecureAuthApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAuthApp.Core.Interfaces
{
    public interface IAuthService
    {
        //promises to return the new User after they sign up
        Task<User> RegisterAsync(string username, string password);

        //promises to return a JWT token ( a string ) if the password is correct
        Task<string> LoginAsync(string username, string password);
    }
}
