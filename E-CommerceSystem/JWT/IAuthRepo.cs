using E_CommerceSystem.DTOs.Auth;
using E_CommerceSystem.Models;

namespace E_CommerceSystem.JWT
{
    public interface IAuthRepo
    {
        Task<string> Login(LoginDTO loginDTO);
        Task<bool> Register(User user);
    }
}
