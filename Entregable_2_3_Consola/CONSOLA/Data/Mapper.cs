using CONSOLA.Models;
using Microsoft.Data.SqlClient;

namespace CONSOLA.Data
{
    public static class Mapper
    {
        //public static Producto MapProducto(SqlDataReader reader)
        //{
        //    return new Producto
        //    {
        //        ProductoID = reader.GetInt32(reader.GetOrdinal("ProductoID")),
        //        NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
        //        Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
        //        Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
        //        CategoriaID = reader.GetInt32(reader.GetOrdinal("CategoriaID"))
        //    };
        //}

        public static PedidoDetalle MapPedidoDetalle(SqlDataReader reader)
        {
            return new PedidoDetalle
            {
                OrdenID = reader.GetInt32(reader.GetOrdinal("OrdenID")),
                Cliente = reader.GetString(reader.GetOrdinal("Cliente")),
                NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total"))
            };
        }






    }
}
