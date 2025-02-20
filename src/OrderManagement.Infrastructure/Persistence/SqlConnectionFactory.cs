using Microsoft.Data.SqlClient;
using OrderManagement.Application.Contract;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Persistence
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqlConnectionFactory(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                //this._connection = new SqlConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }

        public IDbConnection CreateNewConnection()
        {
            //var connection = new SqlConnection(_connectionString);
            _connection.Open();

            return _connection;
        }

        public string GetConnectionString()
        {
            return _connection.ConnectionString;
        }

        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }
    }
}
