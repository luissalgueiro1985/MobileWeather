using MobileWeather.Models;
using System.Threading.Tasks;

namespace MobileWeather.Services
{
    public interface IAPIService
    {
        Task<bool> LoginAsync(string username, string password);

        Task<Response> GetWeather<T>(string location);

        Task<ChangeUser> GetUserAsync(string username);

        Task<ChangeUser> UserChange(string firstName, string lastName, string address, int fiscalNumber, int citizenCardNumber, int age);
    }
}
