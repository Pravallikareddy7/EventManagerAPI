using EventManagerAPI.Dtos;
using EventManagerAPI.Models.Entities;
using System.Threading.Tasks;
using EventManagerAPI.Models;

namespace EventManagerAPI.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDto userRegisterDto);
        Task<string> Login(UserLoginDto userLoginDto);
    }
}

