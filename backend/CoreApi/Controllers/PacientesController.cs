using CoreApi.Data;
using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly AppDbContext _context;
    public PacientesController(AppDbContext context)
    {
        _context = context;
    }




    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var pacientes = await _context.Pacientes.ToListAsync();
        return Ok(pacientes);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PacienteModel paciente)
    {
        if (paciente == null)
            return BadRequest("Paciente inválido.");

        // Adiciona ao DbContext (prepara para salvar)
        await _context.Pacientes.AddAsync(paciente);

        // Salva no banco de dados
        await _context.SaveChangesAsync();

        // Retorna 201 Created com o paciente criado (pode retornar a URI ou o objeto criado)
        return CreatedAtAction(nameof(GetPorId), new { id = paciente.Id }, paciente);
    }

    // Método auxiliar para GetPorId para o CreatedAtAction funcionar
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null) return NotFound();
        return Ok(paciente);
    }


}
