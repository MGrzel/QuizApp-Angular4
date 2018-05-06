using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IAccountService
    {
        Task<User> GetUserByNameAsync(string username);
        Task<User> GetUserFromJwtToken(string token);
    }
}
