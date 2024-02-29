using Workintech02RestApiDemo.Domain.Entities;

namespace Workintech02RestApiDemo.Business.Authentication
{
    public interface IAuthenticationService:IBaseService
    {
        string GenerateToken(User user);
        User Login(string username, string password);
        User Register(User user);
    }
}