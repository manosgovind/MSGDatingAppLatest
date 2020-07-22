using System.Threading.Tasks;
using DatingAppLatest.API.Models;

namespace DatingAppLatest.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);

         Task<User> Login(string userName, string password);

         Task<bool> IsUserExists(string userName);

    }
}