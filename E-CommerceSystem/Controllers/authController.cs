using E_CommerceSystem.DTOs.Auth;
using E_CommerceSystem.Models;
using E_CommerceSystem.UnitOFWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_CommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public authController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var Token = await unitOfWork.AuthRepo.Login(dto);
            if(User == null)
            {
                return Unauthorized("Email Or Password Not Valid");
            }
            return Ok(Token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                Password = dto.Password,
                Role = dto.Role,
            };
            if(await unitOfWork.AuthRepo.Register(user))
            {
                return Created();
            }
            return BadRequest("Email already exists");
        }
    }
}
