using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using AgenciaLimpieza.DataBase;

namespace AgenciaLimpieza.DataBase
{
    public class SQLService
    {
        public static int InsertMethod(string query, Dictionary<string, object?>? param = null)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection())
            {
                try
                {
                    SqlConnection cnx = dbConnection.Connection;
                    query = query.Trim() + "; select SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cnx);
                    if (param != null)
                    {
                        param.ToList().ForEach(x => cmd.Parameters.AddWithValue(x.Key, x.Value ?? DBNull.Value));
                    }
                    cmd.CommandTimeout = 1000;
                    int.TryParse(cmd.ExecuteScalar()?.ToString() ?? "0", out int result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static List<T> SelectMethod<T>(string query)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection())
            {
                List<T> dataList = new();
                try
                {
                    SqlConnection cnx = dbConnection.Connection;
                    SqlCommand cmd = new SqlCommand(query, cnx);
                    cmd.CommandTimeout = 1000;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataList.AddRange(GetItems<T>(reader));
                    }
                    return dataList;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public static IEnumerable<T> GetItems<T>(SqlDataReader reader)
        {
            var properties = typeof(T).GetProperties();
            var columnNames = Enumerable.Range(0, reader.FieldCount)
                                         .Select(i => reader.GetName(i).ToLower())
                                         .ToDictionary(name => name);
            while (reader.Read())
            {

                var item = Activator.CreateInstance<T>();
                foreach (var property in properties)
                {
                    try
                    {
                        var columnName = property.Name.ToLower();
                        if (columnNames.ContainsKey(columnName) && !reader.IsDBNull(columnNames[columnName]))
                        {
                            var value = reader.GetValue(columnNames[columnName]);
                            var type = property.PropertyType.GenericTypeArguments.Length > 0 ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType;
                            var readerType = reader[columnName].GetType();

                            switch (type.Name)
                            {
                                case "String":
                                    property.SetValue(item, value?.ToString()?.Trim());
                                    break;
                                case "Decimal":
                                    if (decimal.TryParse(value?.ToString() ?? "0", out var valueDec))
                                        property.SetValue(item, valueDec);
                                    break;
                                case "Double":
                                    if (double.TryParse(value?.ToString() ?? "0", out var valueDouble))
                                        property.SetValue(item, valueDouble);
                                    break;
                                case "Int32":
                                    if (int.TryParse(value?.ToString() ?? "0", out var valueInt))
                                        property.SetValue(item, valueInt);
                                    break;
                                default:
                                    property.SetValue(item, value);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex.Message);
                    }
                }
                yield return item;
            }
        }

        // Aquí puedes añadir otros métodos como UpdateMethod, DeleteMethod, etc.
    }
}
