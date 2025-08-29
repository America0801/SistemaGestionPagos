namespace SistemaGestionPagos.Domain.Entities
{
    public class Planilla
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoPlanilla { get; set; } = string.Empty;
        public bool Subida { get; set; } = true;
    }
    
}