using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoreApi.Models;
using CoreApi.Data;
using CoreApi.DTO;

namespace CoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // LOGIN MÉDICO
        [HttpPost("medico-login")]
        public IActionResult LoginMedico([FromBody] MedicoModel request)
        {
            var medico = _context.Medicos.FirstOrDefault(m => m.Email == request.Email && m.Senha == request.Senha);
            if (medico == null)
                return Unauthorized("Email ou senha inválidos.");

            var tokenString = GerarToken(medico.Id.ToString(), medico.Email, medico.Nome, "Medico");

            medico.Token = tokenString;
            _context.SaveChanges();

            return Ok(new
            {
                Token = tokenString,
                Medico = new
                {
                    medico.Id,
                    medico.Nome,
                    medico.Email,
                    medico.Status
                }
            });
        }

        // LOGIN PACIENTE
        [HttpPost("paciente-login")]
        public IActionResult LoginPaciente([FromBody] LoginPacienteDTO  request)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.Email == request.Email && p.Senha == request.Senha);
            if (paciente == null)
                return Unauthorized("Email ou senha inválidos.");

            var tokenString = GerarToken(paciente.Id.ToString(), paciente.Email, paciente.Nome, "Paciente");

            paciente.Token = tokenString;
            _context.SaveChanges();

            return Ok(new
            {
                Token = tokenString,
                Paciente = new
                {
                    paciente.Id,
                    paciente.Nome,
                    paciente.Email
                }
            });
        }

        // MÉTODO PRIVADO PARA GERAR O TOKEN
        private string GerarToken(string id, string email, string nome, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
