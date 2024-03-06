using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VokzFeedback.Data;
using VokzFeedback.Models;

namespace VokzFeedback.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : ControllerBase
    {

        private readonly BancoContext _context;

        public UsuarioController(BancoContext context) {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(Guid id)
        {

            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
