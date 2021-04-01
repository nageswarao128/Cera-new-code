using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CERA.AuthenticationService
{
    public interface ICeraClientAuthenticator
    {
        public Task<string> RegisterUser(RegisterUser user);
        public Task<object> LoginUser(LoginModel loginModel);
    }
}
