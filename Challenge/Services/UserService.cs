using Challenge.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Challenge.Services
{
    public class UserService : IUserLogin
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Models.User.LoginResult Login(Models.User.LoginSet loginset)
        {
            Models.User.LoginResult result = new Models.User.LoginResult();
            Models.User.LoginSet validUser = _configuration.GetSection("DummyUser").Get<Models.User.LoginSet>();

            if (validUser.Equals(loginset))
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                            new Claim(ClaimTypes.Name, loginset.User)
                            }),
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    result.IdResponse = StatusCodes.Status200OK;
                    result.Response = "Login OK";
                    result.Token = tokenHandler.WriteToken(token);

                }
                catch(Exception ex)
                {
                    result.IdResponse = StatusCodes.Status500InternalServerError;
                    result.Response = ex.Message;
                }

            }
            else
            {
                result.IdResponse = StatusCodes.Status400BadRequest;
                result.Response = "Usuario o Contraseña incorrectas";
            }


            return result;
        }

    }
}
