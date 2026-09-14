namespace PedidosAPI.Domain.Models
{
    public class PedidoDetalle
    {
        public int OrdenID { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
