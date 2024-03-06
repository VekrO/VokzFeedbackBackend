using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VokzFeedback.Data;
using VokzFeedback.DTOs;
using VokzFeedback.Models;
using VokzFeedback.Services;

namespace VokzFeedback.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly BancoContext _context;
        private readonly TokenService _tokenService;
        
        public AuthenticationController(BancoContext context, TokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO model)
        {
            try
            {

                Usuario usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (usuarioDb != null) {
                    return BadRequest("O e-mail não está disponível!");
                }

                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

                Usuario usuario = new Usuario
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return Ok();

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO model)
        {
            try
            {

                Usuario usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (usuarioDb == null)
                {
                    return NotFound("A conta não foi encontrada ou não existe. Verifique os dados e tente novamente!");
                }

                bool isValid = BCrypt.Net.BCrypt.Verify(model.Password, usuarioDb.Password);

                if (!isValid)
                {
                    return NotFound("Verifique os dados e tente novamente!");
                }

                var token = _tokenService.GenerateToken(usuarioDb);

                return Ok(token);


            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
