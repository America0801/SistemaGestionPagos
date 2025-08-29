using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionPagos.Domain.Entities;
using SistemaGestionPagos.Infrastructure.Persistence;

namespace SistemaGestionPagos.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagosApiController(AppDbContext context)
        {
            _context = context;
        }

        private static DateTime ToUtc(DateTime dt) =>
            dt.Kind switch
            {
                DateTimeKind.Unspecified => DateTime.SpecifyKind(dt, DateTimeKind.Utc),
                DateTimeKind.Local => dt.ToUniversalTime(),
                _ => dt
            };

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] Pago pago, IFormFile? factura, IFormFile? retencion)
        {
            // Normalizar fecha a UTC para Npgsql (timestamptz)
            pago.Fecha = ToUtc(pago.Fecha);

            if (factura != null)
            {
                using var ms = new MemoryStream();
                await factura.CopyToAsync(ms);
                pago.Factura = ms.ToArray();
                pago.NombreFactura = factura.FileName;
            }
            if (retencion != null)
            {
                using var ms = new MemoryStream();
                await retencion.CopyToAsync(ms);
                pago.Retencion = ms.ToArray();
                pago.NombreRetencion = retencion.FileName;
            }

            // Validación contra fecha actual (UTC)
            if (pago.Fecha > DateTime.UtcNow)
            {
                return BadRequest("La fecha ingresada no puede ser superior a la actual");
            }

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Pago registrado correctamente" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound();

            return Ok(new
            {
                pago.Id,
                pago.Fecha,
                pago.Monto,
                pago.Beneficiario,
                pago.TipoPago,
                pago.Descripcion,
                TieneFactura = pago.Factura != null,
                TieneRetencion = pago.Retencion != null
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound();

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Pago eliminado correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromForm] Pago updated, IFormFile? factura, IFormFile? retencion)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound();

            pago.Fecha = ToUtc(updated.Fecha);   // 🔧 normaliza a UTC
            pago.Monto = updated.Monto;
            pago.Beneficiario = updated.Beneficiario;
            pago.TipoPago = updated.TipoPago;
            pago.Descripcion = updated.Descripcion;

            if (factura != null)
            {
                using var ms = new MemoryStream();
                await factura.CopyToAsync(ms);
                pago.Factura = ms.ToArray();
                pago.NombreFactura = factura.FileName;
            }

            if (retencion != null)
            {
                using var ms = new MemoryStream();
                await retencion.CopyToAsync(ms);
                pago.Retencion = ms.ToArray();
                pago.NombreRetencion = retencion.FileName;
            }

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Pago actualizado correctamente" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var pagos = await _context.Pagos
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("ver-factura/{id}")]
        public async Task<IActionResult> VerFactura(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null || pago.Factura == null || string.IsNullOrWhiteSpace(pago.NombreFactura))
                return NotFound();

            var extension = Path.GetExtension(pago.NombreFactura).ToLower();
            var contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
            return File(pago.Factura, contentType);
        }

        [HttpGet("ver-retencion/{id}")]
        public async Task<IActionResult> VerRetencion(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null || pago.Retencion == null || string.IsNullOrWhiteSpace(pago.NombreRetencion))
                return NotFound();

            var extension = Path.GetExtension(pago.NombreRetencion).ToLower();
            var contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
            return File(pago.Retencion, contentType);
        }

        [HttpGet("retencionIVA")]
        public async Task<IActionResult> ObtenerRetencionIVA()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "Retención IVA")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("retencionISR")]
        public async Task<IActionResult> ObtenerRetencionISR()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "Retención ISR")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("planillaLuisRoca")]
        public async Task<IActionResult> ObtenerPlanillaLuis()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "Planilla Luis Roca")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("planillaProyecto")]
        public async Task<IActionResult> ObtenerPlanillaProyecto()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "Planilla Proyecto")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("igss")]
        public async Task<IActionResult> ObtenerIgss()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "IGSS")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("agua")]
        public async Task<IActionResult> ObtenerAgua()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "Servicio de Agua")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("basura")]
        public async Task<IActionResult> ObtenerBasura()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "Servicio de Basura")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("renta")]
        public async Task<IActionResult> ObtenerRenta()
        {
            var pagos = await _context.Pagos
                .Where(p => p.TipoPago == "Pago de Renta")
                .OrderByDescending(p => p.Fecha)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.Monto,
                    p.Beneficiario,
                    p.TipoPago,
                    p.Descripcion,
                    TieneFactura = p.Factura != null,
                    TieneRetencion = p.Retencion != null
                })
                .ToListAsync();

            return Ok(pagos);
        }
    }
}