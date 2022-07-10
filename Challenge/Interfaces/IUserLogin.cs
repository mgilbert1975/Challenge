using Challenge.Models;

namespace Challenge.Interfaces
{
    public interface IUserLogin
    {
        User.LoginResult Login(User.LoginSet loginset);
    }
}
