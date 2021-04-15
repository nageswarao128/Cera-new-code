using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CERA.AuthenticationService
{
    public interface ICeraClientAuthenticator
    {
        public Task<string> AddUser(RegisterUser user);
        public Task<object> Login(LoginModel loginModel);
        public List<UserModel> GetUsers();
        public Task<UserModel> GetUser(string id);
        public Task<object> UpdateUser(UpdateUserModel userModel);
        public Task<object> DeleteUser(string id);
    }
}
