using Microsoft.Data.SqlClient;


namespace CONSOLA.Data
{
    public static class DbConnection
    {
        private const string ConnectionString =
            "Server=localhost;Database=BD_NET;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection Create()
        {
            return new SqlConnection(ConnectionString);
        }
    }

}