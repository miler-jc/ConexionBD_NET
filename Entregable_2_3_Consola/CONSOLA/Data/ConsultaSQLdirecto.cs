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
    public sealed class ConsultaSQLdirecto
    {
        #region Consulta
        public async Task<IReadOnlyList<PedidoDetalle>> PedidoDetalleAsync()
        {
            const string query = """             
            SELECT [OrdenID]
                 ,[Cliente]
                 ,[NombreProducto]
                 ,[Cantidad]
                 ,[PrecioUnitario]
                 ,[Total]
            FROM [BD_NET].[dbo].[PEDIDOS] WITH(NOLOCK)              
            """;

            var PedidoFinal = new List<PedidoDetalle>();

            await using var conn = DbConnection.Create();
            await conn.OpenAsync();

            await using var command = new SqlCommand(query, conn)
            {
                CommandType = CommandType.Text
            };

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                PedidoFinal.Add(Mapper.MapPedidoDetalle(reader));
            }
             
            return PedidoFinal;
        }
        #endregion

        #region Insertar
        public async Task<int> InsertarAsync(PedidoInput pedido)
        {
            const string query = """
            DECLARE @Total DECIMAL(10, 2) = @Cantidad * @PrecioUnitario;
            INSERT INTO BD_NET.dbo.PEDIDOS (Cliente, NombreProducto, Cantidad, PrecioUnitario, Total)
            VALUES (@Cliente, @NombreProducto, @Cantidad, @PrecioUnitario, @Total);

            SELECT CAST(SCOPE_IDENTITY() AS INT) AS OrdenID;
            """;

            await using var conn = DbConnection.Create();
            await conn.OpenAsync();

            await using var command = new SqlCommand(query, conn);

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
            const string query = """
            UPDATE BD_NET.dbo.PEDIDOS
            SET Cliente = @Cliente,
                NombreProducto = @NombreProducto,
                Cantidad = @Cantidad,
                PrecioUnitario = @PrecioUnitario,
                Total = @Cantidad * @PrecioUnitario
            WHERE OrdenID = @OrdenID;
            """;

            await using var conn = DbConnection.Create();
            await conn.OpenAsync();

            await using var command = new SqlCommand(query, conn);

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
            const string query = """
            DELETE FROM BD_NET.dbo.PEDIDOS
            WHERE OrdenID = @OrdenID;
            """;
            await using var conn = DbConnection.Create();
            await conn.OpenAsync();
            await using var command = new SqlCommand(query, conn);
            command.Parameters.Add("@OrdenID", SqlDbType.Int).Value = ordenId;
            return await command.ExecuteNonQueryAsync();
        }
        #endregion

    }
}
