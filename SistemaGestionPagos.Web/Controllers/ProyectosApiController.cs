using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionPagos.Domain.Entities;
using SistemaGestionPagos.Infrastructure.Persistence;

namespace SistemaGestionPagos.Web.Controllers
{
    [ApiController]
    [Route("api/proyectos")]
    public class ProyectosApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProyectosApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var proyectos = await _context.Proyectos
                .OrderByDescending(p => p.FechaInicio)
                .ToListAsync();

            return Ok(proyectos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Proyecto proyecto)
        {
            if (string.IsNullOrWhiteSpace(proyecto.Nombre))
                return BadRequest("Nombre requerido");

            proyecto.Estado = "en proceso";
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proyecto creado", proyecto.Id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDetalle(int id)
        {
            var proyecto = await _context.Proyectos
                .Include(p => p.Movimientos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proyecto == null) return NotFound();
            return Ok(new
            {
                proyecto.Id,
                proyecto.Nombre,
                proyecto.FechaInicio,
                proyecto.Valor,
                proyecto.Estado,
                movimientos = proyecto.Movimientos.Select(m => new
                {
                    m.Id,
                    m.Fecha,
                    m.Descripcion,
                    tieneDocumento = m.Documento != null
                })
            });
        }

        [HttpPost("{id}/movimientos")]
        public async Task<IActionResult> AgregarMovimiento(int id, [FromForm] MovimientoProyecto movimiento, IFormFile? documento)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null) return NotFound();

            movimiento.ProyectoId = id;

            if (documento != null)
            {
                using var ms = new MemoryStream();
                await documento.CopyToAsync(ms);
                movimiento.Documento = ms.ToArray();
                movimiento.NombreDocumento = documento.FileName;
            }

            _context.MovimientosProyecto.Add(movimiento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                proyecto.Id,
                proyecto.Nombre,
                proyecto.FechaInicio,
                proyecto.Valor,
                proyecto.Estado,
                movimientos = proyecto.Movimientos.Select(m => new
                {
                    m.Id,
                    m.Fecha,
                    m.Descripcion,
                    TieneDocumento = m.Documento != null
                })
            });
        }

        [HttpPut("{id}/finalizar")]
        public async Task<IActionResult> FinalizarProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null) return NotFound();

            proyecto.Estado = "finalizado";
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proyecto finalizado" });
        }

        [HttpPut("{id}/reanudar")]
        public async Task<IActionResult> ReanudarProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null) return NotFound();

            proyecto.Estado = "en proceso";
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proyecto Reanudado" });
        }

        [HttpGet("movimiento/{movId}/ver")]
        public async Task<IActionResult> VerDocumentoMovimiento(int movId)
        {
            var movimiento = await _context.MovimientosProyecto.FindAsync(movId);
            if (movimiento == null || movimiento.Documento == null || string.IsNullOrWhiteSpace(movimiento.NombreDocumento))
                return NotFound();

            var extension = Path.GetExtension(movimiento.NombreDocumento).ToLower();
            var contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };

            return File(movimiento.Documento, contentType);
        }
    }
}
