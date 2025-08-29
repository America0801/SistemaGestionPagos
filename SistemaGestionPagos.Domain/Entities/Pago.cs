namespace SistemaGestionPagos.Domain.Entities
{
    public class Pago
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Beneficiario { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string TipoPago { get; set; } = string.Empty;
        public byte[]? Factura { get; set; }
        public string? NombreFactura { get; set; }
        public byte[]? Retencion { get; set; }
        public string? NombreRetencion { get; set; }
    }
}
