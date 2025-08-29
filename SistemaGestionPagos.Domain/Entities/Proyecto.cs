namespace SistemaGestionPagos.Domain.Entities
{
    public class Proyecto
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Estado { get; set; } = "En proceso";

        public ICollection<MovimientoProyecto> Movimientos { get; set; } = new List<MovimientoProyecto>();
    }
}
