using Microsoft.Data.SqlClient;

namespace PedidosAPI.Data.Dapper
{
     public interface ISQLDapperBD
    {
        SqlConnection Create();

     }
}
