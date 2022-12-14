using Project.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;

namespace Projec.Data.Repository
{
    public partial class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private SqlConnection _connection;
        private int _CommantTimeout = 300;

        /// <summary>
        /// Constructor to init MySQL connection
        /// </summary>
        public BaseRepository()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlDBConnection"].ConnectionString);
        }

        /// <summary>
        /// map result set to Entity
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="propertyKeyMap"></param>
        /// <returns></returns>
        public virtual T PopulateRecord(SqlDataReader reader, IDictionary<string, string> propertyKeyMap = null)
        {
            if (reader != null)
            {
                var entity = GetInstance<T>();
                if (propertyKeyMap == null)
                {
                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        if (HasColumn(reader, prop.Name))
                        {
                            if (reader[prop.Name] != DBNull.Value)
                            {
                                if (prop != null)
                                {
                                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                    object safeValue = (reader[prop.Name] == null) ? null : Convert.ChangeType(reader[prop.Name], t);

                                    prop.SetValue(entity, safeValue, null);
                                }

                                //Type propType = prop.PropertyType;
                                //prop.SetValue(entity, Convert.ChangeType(reader[prop.Name], propType), null);
                            }
                        }
                    }
                    return entity;
                }
                else
                {
                    foreach (var propkey in propertyKeyMap)
                    {
                        var prop = entity.GetType().GetProperties().Where(m => m.Name.ToLower() == propkey.Key.ToLower()).FirstOrDefault();
                        if (HasColumn(reader, propkey.Value) && prop != null)
                        {
                            if (reader[propkey.Value] != DBNull.Value)
                            {
                                if (prop != null)
                                {
                                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                    object safeValue = (reader[prop.Name] == null) ? null : Convert.ChangeType(reader[prop.Name], t);

                                    prop.SetValue(entity, safeValue, null);
                                }

                                //Type propType = prop.PropertyType;
                                //prop.SetValue(entity, Convert.ChangeType(reader[propkey.Value], propType), null);
                            }
                        }
                    }
                    return entity;
                }
            }
            return GetInstance<T>();
        }


        /// <summary>
        /// Get the istance of the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected To GetInstance<To>()
        {
            return (To)FormatterServices.GetUninitializedObject(typeof(T));
        }


        /// <summary>
        /// Check if the coloum exsist in the Datareader
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// get array of records for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public IEnumerable<T> GetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

        /// <summary>
        /// get record for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public T GetRecord(SqlCommand command)
        {
            T record = null;
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PopulateRecord(reader);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return record;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProc(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            command.CommandType = CommandType.StoredProcedure;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

        /// <summary>
        /// get array of records for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public IEnumerable<T> GetRecords(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            var list = new List<T>();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader, propertyMap));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

        /// <summary>
        /// get record for a entity
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public T GetRecord(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            T record = null;
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PopulateRecord(reader, propertyMap);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return record;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return Datatable with all records</returns>
        public DataTable ExecuteStoredProcedure(SqlCommand command)
        {
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            command.CommandType = CommandType.StoredProcedure;
            _connection.Open();
            try
            {
                try
                {
                    using (reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return table;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <param name="propertyMap">Maping of Class property to reader coloum.</param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProc(SqlCommand command, IDictionary<string, string> propertyMap)
        {
            var list = new List<T>();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            command.CommandType = CommandType.StoredProcedure;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader, propertyMap);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public int ExecuteProc(SqlCommand command)
        {
            int rowsAffected;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            command.CommandType = CommandType.StoredProcedure;
            //_connection.Open();
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                rowsAffected = Convert.ToInt32(command.ExecuteScalar());
            }
            finally
            {
                _connection.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public int ExecuteNonQueryProc(SqlCommand command)
        {
            int rowsAffected;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            command.CommandType = CommandType.StoredProcedure;
            //_connection.Open();
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Execute A procedure
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns>Return affected records count</returns>
        public object ExecuteProcedure(SqlCommand command)
        {
            object returnObj;
            IDataReader reader = null;
            DataTable table = new DataTable();
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            command.CommandType = CommandType.StoredProcedure;
            //_connection.Open();
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _connection.Close();
            }

            return returnObj;
        }

        /// <summary>
        /// Execute A query
        /// </summary>
        /// <param name="command"> or query.</param>
        /// <returns>Return object</returns>
        public object ExecuteQuery(SqlCommand command)
        {
            object returnObj;
            //IDataReader reader = null;
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            //_connection.Open();
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                returnObj = command.ExecuteScalar();
            }
            finally
            {
                _connection.Close();
            }

            return returnObj;
        }

        ///Below 2 methods Added by Keshaw
        ///
        /// <summary>
        /// Execute A Query
        /// </summary>
        /// <param name="command">Sql Command with parameters or query.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query)
        {
            //var list = new List<T>();
            int rowsAdded;
            SqlCommand command = new SqlCommand(query);
            command.Connection = _connection;
            command.CommandTimeout = _CommantTimeout;
            //command.CommandType = CommandType.;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            // _connection.Open();
            try
            {
                rowsAdded = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
            return rowsAdded;
        }

        /// <summary>
        /// Get Cache Key using id patern and Mysql command
        /// </summary>
        /// <param name="KeyPrefix">Prefix used for key like AreaName.ServiceName</param>
        /// <param name="KeyId">Id for the key like StockId or IndiceId</param>
        /// <param name="CMD">Command object For parameters</param>
        /// <returns></returns>
        public string GetCacheKey(string KeyPrefix, object KeyId, SqlCommand CMD)
        {
            return GetCacheKey(string.Format("{0}.{1}", KeyPrefix, KeyId), CMD).Replace("/", string.Empty).Replace(":", string.Empty);
        }

        /// <summary>
        /// Get Cache Key using id patern and Mysql command
        /// </summary>
        /// <param name="KeyPrefix">Prefix used for key like AreaName.ServiceName</param>
        /// <param name="KeyId">Id for the key like StockId or IndiceId</param>
        /// <param name="Parameter">Command object For parameters</param>
        /// <returns></returns>
        public string GetCacheKey(string KeyPrefix, object KeyId, string Parameter)
        {
            return string.Format("{0}.{1}.{2}", KeyPrefix, KeyId, Parameter).Replace("/", string.Empty).Replace(":", string.Empty);
        }

        /// <summary>
        /// Get Cache Key using id patern and Mysql command
        /// </summary>
        /// <param name="KeyPrefix">Prefix used for key like AreaName.ServiceName</param>
        /// <param name="CMD">Command object For parameters</param>
        public string GetCacheKey(string KeyPrefix, SqlCommand CMD)
        {
            string key = string.Format("{0}.{1}", KeyPrefix, CMD.CommandText);
            foreach (SqlParameter item in CMD.Parameters)
            {
                key = string.Format("{0}.{1}", key, Convert.ToString(item.Value).Replace(" ", string.Empty));
            }
            return key.Replace("/", string.Empty).Replace(":", string.Empty);
        }

        /// <summary>
        /// Get Cache Key using id patern and Mysql command
        /// </summary>
        /// <param name="KeyPrefix">Prefix used for key like AreaName.ServiceName</param>
        /// <param name="KeyId">Id for the key like StockId or IndiceId</param>
        /// <returns></returns>
        public string GetCacheKey(string KeyPrefix, object KeyId)
        {
            return string.Format("{0}.{1}", KeyPrefix, KeyId).Replace("/", string.Empty).Replace(":", string.Empty);
        }
    }
}
