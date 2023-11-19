using System;
using System.Data.SqlClient;

namespace AgenciaLimpieza.DataBase
{
    public class DatabaseConnection : IDisposable
    {
        private SqlConnection _connection;
        private bool _disposed = false; // Para rastrear si Dispose ha sido llamado

        private string _connectionString = "Server=NITRO-5\\SQLEXPRESS;Database=GestionLimpieza;Trusted_Connection=True;";

        public DatabaseConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        // Implementación del método Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Liberar recursos administrados
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection = null;
                    }
                }

                // Aquí puedes liberar recursos no administrados si los hay

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Evita que se llame al destructor
        }

        // Destructor
        ~DatabaseConnection()
        {
            Dispose(false);
        }
    }
}
