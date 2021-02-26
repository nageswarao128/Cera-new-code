using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERAAPI.BusinessLogic
{
    public class Validator
    {
        private IConfiguration _config { get; set; }
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        public Validator(IConfiguration config)
        {
            _config = config;
        }
        string appSettingsKey { get; set; }
        byte[] key;
        public void InitializeVars()
        {
            appSettingsKey = _config.GetSection("JWT:key").Value;
            key = Encoding.ASCII.GetBytes(appSettingsKey);
        }
        public (string email, string userId, string orgname) ValidUser(string JwtToken)
        {
            InitializeVars();
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(JwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var email = jwtToken.Claims.First(x => x.Type == "email").Value;
            var userId = jwtToken.Claims.First(x => x.Type == "nameid").Value;
            var orgname = jwtToken.Claims.First(x => x.Type == "orgname").Value;
            return (email, userId, orgname);
        }
    }
}
