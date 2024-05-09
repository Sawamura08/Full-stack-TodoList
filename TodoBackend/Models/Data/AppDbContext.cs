using Npgsql;
using System.Data;

namespace TodoBackend.Models.Data
{
    public class AppDbContext
    {
        private readonly IConfiguration _config;
        private readonly string _ConnectionString;

        public AppDbContext(IConfiguration config)
        {
            _config = config;
            _ConnectionString = _config.GetConnectionString("Connection") ?? throw new InvalidOperationException("Connection string is null.");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_ConnectionString);
    }
}
