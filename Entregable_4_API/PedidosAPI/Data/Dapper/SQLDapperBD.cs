using Microsoft.Data.SqlClient;

namespace PedidosAPI.Data.Dapper
{
    public sealed class SQLDapperBD(IConfiguration configuration) : ISQLDapperBD
    {

        public SqlConnection Create()
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "No se encontró la cadena de conexión 'DefaultConnection'.");

            return new SqlConnection(connectionString);
        }
    }
}
