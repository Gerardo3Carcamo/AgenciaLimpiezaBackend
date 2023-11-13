using System;
using System.Data.SqlClient;
namespace AgenciaLimpieza.DataBase
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private static readonly object _lock = new object();
        private SqlConnection _connection;

        private string _connectionString = "Server=localhost\\SQLEXPRESS;Database=GestionLimpieza;Trusted_Connection=True;";

        private DatabaseConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        public static DatabaseConnection Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection();
                    }else if(_instance._connection.State == System.Data.ConnectionState.Closed)
                    {
                        _instance._connection.Open();
                    }
                    return _instance;
                }
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
