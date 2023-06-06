using System.Data;
using System.Data.SqlClient;

namespace Api_Properties.DataContext
{
    public class DapperContext
    {
        private readonly IConfiguration _config;
        private readonly string connectionSql;

        public DapperContext(IConfiguration configuration)         
        {
            _config = configuration;
            connectionSql = _config["ConnectionStrings:SqlServerConnection"];
        }

        public IDbConnection CreateConnection() =>
            new SqlConnection(connectionSql);
    }
}
