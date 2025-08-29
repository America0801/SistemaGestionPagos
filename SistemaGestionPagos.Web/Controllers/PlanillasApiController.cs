using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionPagos.Domain.Entities;
using SistemaGestionPagos.Infrastructure.Persistence;

namespace SistemaGestionPagos.Web.Controllers
{
    [ApiController]
    [Route("api/planillas")]
    public class PlanillasApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanillasApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PlanillasApi
        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var planillas = await _context.Planillas
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            return Ok(planillas);
        }

        // POST: api/PlanillasApi
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Planilla nuevaPlanilla)
        {
            if (nuevaPlanilla == null || string.IsNullOrWhiteSpace(nuevaPlanilla.TipoPlanilla))
            {
                return BadRequest("Datos inválidos.");
            }

            _context.Planillas.Add(nuevaPlanilla);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Planilla registrada correctamente", nuevaPlanilla.Id });
        }
    }
}
