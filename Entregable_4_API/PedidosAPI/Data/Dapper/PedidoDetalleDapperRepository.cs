using Dapper;
using Microsoft.Data.SqlClient;
using PedidosAPI.Domain.Models;

namespace PedidosAPI.Data.Dapper
{
    public sealed class PedidoDetalleDapperRepository(ISQLDapperBD conn)
    {
        public async Task<IEnumerable<PedidoDetalle>> ListarPedidosAsync()
        {
            await using var connection = conn.Create();
            await connection.OpenAsync();

            var sql = """
            SELECT * FROM dbo.PEDIDOS
            """;

            return await connection.QueryAsync<PedidoDetalle>(sql);
        }


        public async Task<int> InsertAsync(PedidoDetalle detalle)
        {
            await using var connection = conn.Create();
            await connection.OpenAsync();

            var sql = """
            DECLARE @Calculo DECIMAL(10, 2) = @Cantidad * @PrecioUnitario;
            INSERT INTO BD_NET.dbo.PEDIDOS (Cliente, NombreProducto, Cantidad, PrecioUnitario, Total)
            VALUES (@Cliente, @NombreProducto, @Cantidad, @PrecioUnitario, @Calculo);

            SELECT CAST(SCOPE_IDENTITY() AS INT) AS OrdenID;
            """;

            var id = await connection.QuerySingleAsync<int>(sql, detalle);
            return id;
        }


        public async Task<int> ActualizarPedido(PedidoDetalle detalle)
        {
            await using var connection = conn.Create();
            await connection.OpenAsync();

            var sql = """
            UPDATE BD_NET.dbo.PEDIDOS
            SET Cliente = @Cliente,
                NombreProducto = @NombreProducto,
                Cantidad = @Cantidad,
                PrecioUnitario = @PrecioUnitario,
                Total = @Cantidad * @PrecioUnitario
            WHERE OrdenID = @OrdenID;

            SELECT @OrdenID;
            """;

            var id = await connection.QuerySingleAsync<int>(sql, detalle);
            return id;

        }

        public async Task<int> EliminarPedido(PedidoDetalle detalle)
        {
            await using var connection = conn.Create();
            await connection.OpenAsync();

            var sql = """
            DELETE FROM BD_NET.dbo.PEDIDOS
            WHERE OrdenID = @OrdenID;
            """;

            return await connection.ExecuteAsync(sql, detalle);
            
        }




    }
}
