using SistemaGestionPagos.Domain.Entities;
public class MovimientoProyecto
{
    public int Id { get; set; }
    public int ProyectoId { get; set; }
    public Proyecto? Proyecto { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public byte[]? Documento { get; set; }
    public string? NombreDocumento { get; set; }
}