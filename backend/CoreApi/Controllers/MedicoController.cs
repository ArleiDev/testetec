using CoreApi.Data;
using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MedicoController : ControllerBase
{
    private readonly AppDbContext _context;
    public MedicoController(AppDbContext context)
    {
        _context = context;
    }




    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var medicos = await _context.Medicos.ToListAsync();
        return Ok(medicos);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MedicoModel medico)
    {
        if (medico == null)
            return BadRequest("medico inválido.");

        // Adiciona ao DbContext (prepara para salvar)
        await _context.Medicos.AddAsync(medico);

        // Salva no banco de dados
        await _context.SaveChangesAsync();

        // Retorna 201 Created com o medico criado (pode retornar a URI ou o objeto criado)
        return CreatedAtAction(nameof(GetPorId), new { id = medico.Id }, medico);
    }

    // Método auxiliar para GetPorId para o CreatedAtAction funcionar
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return NotFound();
        return Ok(medico);
    }


}
