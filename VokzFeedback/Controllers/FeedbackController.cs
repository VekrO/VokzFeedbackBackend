using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VokzFeedback.Data;
using VokzFeedback.DTOs;
using VokzFeedback.Models;

namespace VokzFeedback.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeedbackController : ControllerBase
    {

        private readonly BancoContext _context;
        public FeedbackController(BancoContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> Get(Guid id, Guid idUsuario) {
        
            try
            {

                Feedback feedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id && x.User.Id == idUsuario);

                if (feedback == null)
                {
                    return NotFound();
                }

                return Ok(feedback);

            } catch (Exception ex) {

                throw new Exception(ex.Message);

            }
        
        }

        [HttpGet("usuario/{idUsuario}/status/{status}")]
        public async Task<ActionResult<Feedback[]>> GetByUsuarioAndStatus(Guid idUsuario, string status)
        {
            try {
                    
                if(status == "Todos")
                {
                    return Ok(await _context.Feedbacks.Where(x => x.User.Id == idUsuario).ToListAsync());
                }

                return Ok(await _context.Feedbacks.Where(x => x.User.Id == idUsuario && x.Status == status).ToListAsync());

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(FeedbackDTO model)
        {
            try
            {

                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == model.UserId);

                if(usuario == null)
                {
                    return NotFound("A conta não foi encontrada!");
                }

                Feedback feedback = new Feedback
                {
                    Description = model.Description,
                    Sender = model.Sender,
                    UserId = usuario.Id,
                    User = usuario,
                    Status = model.Status
                };

                await _context.Feedbacks.AddAsync(feedback);
                await _context.SaveChangesAsync();

                return Ok(feedback);

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            try
            {
                
                Feedback feeback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);

                if(feeback == null)
                {
                    return NotFound("Registro não encontrado!");
                }

                _context.Feedbacks.Remove(feeback);
                await _context.SaveChangesAsync();
                return Ok();

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }

        }

    }
}
