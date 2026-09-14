using CONSOLA.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONSOLA.Data
{
    public sealed class ConsultaSP
    {
        #region Consulta
        public async Task<IReadOnlyList<PedidoDetalle>> PedidoDetalleAsync()
        {
            var Pedidos = new List<PedidoDetalle>();

            await using var conn = DbConnection.Create();
            await conn.OpenAsync();

            await using var command = new SqlCommand("dbo.SP_ConsultarPedido", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Pedidos.Add(Mapper.MapPedidoDetalle(reader));
            }

            return Pedidos;
        }
        #endregion

        #region Insertar
        public async Task<int> InsertarAsync(PedidoInput pedido)
        {
            await using var conn = DbConnection.Create();
            await conn.OpenAsync();

            await using var command = new SqlCommand("dbo.SP_Insertar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Cliente", SqlDbType.NVarChar, 100).Value = pedido.Cliente;
            command.Parameters.Add("@NombreProducto", SqlDbType.NVarChar, 100).Value = pedido.NombreProducto;
            command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = pedido.Cantidad;
            command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = pedido.PrecioUnitario;

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        #endregion

        #region Actualizar
        public async Task<int> ActualizarAsync(int ordenId, PedidoInput actualizarpedido)
        {
            await using var conn = DbConnection.Create();
            await conn.OpenAsync();

            await using var command = new SqlCommand("dbo.SP_Actualizar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@OrdenID", SqlDbType.Int).Value = ordenId;
            command.Parameters.Add("@Cliente", SqlDbType.NVarChar, 100).Value = actualizarpedido.Cliente;
            command.Parameters.Add("@NombreProducto", SqlDbType.NVarChar, 100).Value = actualizarpedido.NombreProducto;
            command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = actualizarpedido.Cantidad;
            command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = actualizarpedido.PrecioUnitario;

            return await command.ExecuteNonQueryAsync();
        }
        #endregion
        #region Eliminar
        public async Task<int> EliminarAsync(int ordenId)
        {
            await using var conn = DbConnection.Create();
            await conn.OpenAsync();
            await using var command = new SqlCommand("dbo.SP_Eliminar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@OrdenID", SqlDbType.Int).Value = ordenId;
            return await command.ExecuteNonQueryAsync();
        }
        #endregion



    }
}




