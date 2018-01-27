using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProctorApiv2.Repositories
{
    public abstract class BaseSqlRepository
    {
        public readonly string _connStr;

        public BaseSqlRepository()
        {
            _connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        /// <summary>
        /// Retrieves a field value from the Data objects
        /// </summary>
        /// <param name="fieldName">Name of the field in the dataset</param>
        /// <returns>boolean value for the field</returns>
        protected bool BooleanValue(object value)
        {
            return value == null || value == System.DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt32(value));
        }


        /// <summary>
        /// Returns a SqlParameter object for a bool
        /// </summary>
        /// <param name="paramenterName">Name of the Sql parameter in the query string</param>
        /// <param name="value">Bool value to save to the database</param>
        /// <returns>SqlParameters for the field</returns>
        protected SqlParameter BooleanParameter(string parameterName, bool value)
        {
            return new SqlParameter(parameterName, value);
        }
        protected SqlParameter IntegerParameter(string parameterName, int value)
        {
            return value == -1
                ? new SqlParameter(parameterName, DBNull.Value)
                : new SqlParameter(parameterName, value);
        }


        /// <summary>
        /// Retrieves a field value from the Data objects
        /// </summary>
        /// <param name="fieldName">Name of the field in the dataset</param>
        /// <returns>Integer value for the field</returns>
        protected int IntegerValue(object value)
        {
            return value == null || value == System.DBNull.Value ? 0 : Convert.ToInt32(value);
        }


        /// <summary>
        /// Returns a SqlParameter object for a decimal
        /// </summary>
        /// <param name="paramenterName">Name of the Sql parameter in the query string</param>
        /// <param name="value">Decimal value to save to the database</param>
        /// <returns>Decimal value for the field</returns>
        protected SqlParameter DecimalParameter(string parameterName, decimal value)
        {
            return value == -1
                ? new SqlParameter(parameterName, DBNull.Value)
                : new SqlParameter(parameterName, value);
        }


        /// <summary>
        /// Retrieves a field value from the Data objects
        /// </summary>
        /// <param name="fieldName">Name of the field in the dataset</param>
        /// <returns>Decimal value for the field</returns>
        protected decimal DecimalValue(object value)
        {
            return value == null || value == System.DBNull.Value ? 0 : Convert.ToDecimal(value);
        }


        /// <summary>
        /// Returns a SqlParameter object for a string
        /// </summary>
        /// <param name="paramenterName">Name of the Sql parameter in the query string</param>
        /// <param name="value">String value to save to the database</param>
        /// <returns>String value for the field</returns>
        protected SqlParameter StringParameter(string parameterName, string value)
        {
            return string.IsNullOrEmpty(value)
                ? new SqlParameter(parameterName, DBNull.Value)
                : new SqlParameter(parameterName, value);
        }


        /// <summary>
        /// Retrieves a field value from the Data objects
        /// </summary>
        /// <param name="fieldName">Name of the field in the dataset</param>
        /// <returns>String value for the field</returns>
        protected string StringValue(object value)
        {
            return value == null || value == System.DBNull.Value ? string.Empty : value.ToString();
        }


        /// <summary>
        /// Returns a SqlParameter object for a DateTime
        /// </summary>
        /// <param name="paramenterName">Name of the Sql parameter in the query string</param>
        /// <param name="value">String value to save to the database</param>
        /// <returns>String value for the field</returns>
        protected SqlParameter DateTimeParameter(string parameterName, DateTime value)
        {
            return value == DateTime.MinValue
                ? new SqlParameter(parameterName, DBNull.Value)
                : new SqlParameter(parameterName, value);
        }


        /// <summary>
        /// Returns a SqlParameter object for a DateTime
        /// </summary>
        /// <param name="paramenterName">Name of the Sql parameter in the query string</param>
        /// <param name="value">String value to save to the database</param>
        /// <returns>String value for the field</returns>
        protected SqlParameter DateTimeParameter(string parameterName, DateTime? value)
        {
            return value == null
                ? new SqlParameter(parameterName, DBNull.Value)
                : new SqlParameter(parameterName, value);
        }


        /// <summary>
        /// Retrieves a field value from the Data objects
        /// </summary>
        /// <param name="fieldName">Name of the field in the dataset</param>
        /// <returns>DateTime value for the field</returns>
        protected DateTime DateTimeValue(object value)
        {
            return value == null || value == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(value);
        }


        /// <summary>
        /// Retrieves a field value from the Data objects
        /// </summary>
        /// <param name="fieldName">Name of the field in the dataset</param>
        /// <returns>DateTime? value for the field</returns>
        protected DateTime? NullableDateTimeValue(object value)
        {
            if (value == null || value == System.DBNull.Value)
            {
                return null;
            }
            else
            {
                DateTime dtHolder;
                if (DateTime.TryParse(value.ToString(), out dtHolder))
                {
                    return dtHolder;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<T> GetFromSQL<T>(string connStr, string sql, Func<SqlDataReader, T> converter, List<SqlParameter> parameters)
        {
            List<T> retval = new List<T>();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retval.Add(converter(reader));
                }
            }

            return retval;
        }

        public T GetFromSQLSingle<T>(string connStr, string sql, Func<SqlDataReader, T> converter)
        {
            return GetFromSQLSingle<T>(connStr, sql, converter, new List<SqlParameter>());
        }

        public T GetFromSQLSingle<T>(string connStr, string sql, Func<SqlDataReader, T> converter, List<SqlParameter> parameters)
        {
            

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }

                var reader = cmd.ExecuteReader();
                reader.Read();
                return converter(reader);
            }
            
        }

        public List<T> GetFromSQL<T>(string connStr, string sql, Func<SqlDataReader, T> converter)
        {
            return GetFromSQL<T>(connStr, sql, converter, new List<SqlParameter>());
        }

        public void ExecuteStatement(string connStr, Action<SqlConnection, SqlCommand> f)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Connection = conn;

                f(conn, cmd);

                if (string.IsNullOrEmpty(cmd.CommandText))
                {
                    throw new Exception("Please enter command text");
                }

                cmd.ExecuteNonQuery();
            }
        }

        public int ExecuteScalerStatement(string connStr, Action<SqlConnection, SqlCommand> f)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Connection = conn;

                f(conn, cmd);

                if (string.IsNullOrEmpty(cmd.CommandText))
                {
                    throw new Exception("Please enter command text");
                }

                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }


        public T AutoConvert<T>(SqlDataReader reader) where T : new()
        {
            T ret = new T();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);

                PropertyInfo pi = typeof(T).GetProperty(columnName);

                object v = reader[columnName];

                if (pi != null && v != DBNull.Value)
                {
                    if (Nullable.GetUnderlyingType(pi.PropertyType) == null)
                    {
                        pi.SetValue(ret, Convert.ChangeType(v, pi.PropertyType), null);
                    }
                    else
                    {
                        //Property is a nullable type.
                        pi.SetValue(ret, Convert.ChangeType(v, Nullable.GetUnderlyingType(pi.PropertyType)), null);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Retrieves a field value from the Data objects
        /// </summary>
        /// <param name="fieldName">Name of the field in the dataset</param>
        /// <returns>Double value for the field</returns>
        protected double DoubleValue(object value)
        {
            return value == null || value == System.DBNull.Value ? 0 : Convert.ToDouble(value);
        }
    }
}