using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DB_con
{
    public enum DBTrans
    #region "enum"
    {
        Insert,
        Update
    }
    #endregion

    public class ConnectionCls
    {
        #region "variable"
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        SqlTransaction trans;
        #endregion


        public ConnectionCls()
        {
            //
            // TODO: Add constructor logic here
            //
            createConnection();
            setconnection();
        }

        public void closeConnection()
        {
            try
            {
                if (trans != null)
                {
                    try
                    {
                        trans.Commit();
                        trans = null;
                    }
                    catch (Exception)
                    { }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                else if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void createConnection()
        {
            try
            {
                string str = WebConfigurationManager.ConnectionStrings["sqlconstr"].ConnectionString;
                conn = new SqlConnection(str);
                cmd = new SqlCommand();
                cmd.CommandTimeout = 60 * 10;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void createConnection(Boolean mybool)
        {
            try
            {
                string str = WebConfigurationManager.ConnectionStrings["sqlconstr"].ConnectionString;
                conn = new SqlConnection(str);
                cmd = new SqlCommand();
                cmd.CommandTimeout = 60 * 10;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void setCommandTimeout(Int32 minutes)
        {
            try
            {
                cmd.CommandTimeout = 60 * minutes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setconnection()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void clearParameter()
        {
            try
            {
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void addParameter(string name, object value)
        {
            try
            {
                cmd.Parameters.AddWithValue(name, value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void addParameter(string name, long value, DB_con.DBTrans trans)
        {
            try
            {
                if (trans == DBTrans.Insert)
                {
                    cmd.Parameters.AddWithValue(name, value);
                    cmd.Parameters[name].Direction = ParameterDirection.Output;
                }
                else if (trans == DBTrans.Update)
                    cmd.Parameters.AddWithValue(name, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void addParameter(string name, string value, DB_con.DBTrans trans)
        {
            try
            {
                if (trans == DBTrans.Insert)
                {
                    cmd.Parameters.AddWithValue(name, value);
                    //cmd.Parameters[name].Direction = ParameterDirection.Output;
                }
                else if (trans == DBTrans.Update)
                    cmd.Parameters.AddWithValue(name, value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AddOutputParaMeter(string name, string value, DB_con.DBTrans trans)
        {
            try
            {

                cmd.Parameters.AddWithValue(name, value);
                cmd.Parameters[name].Direction = ParameterDirection.Output;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Adds an Output parameter to the existing command object
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="dbType">Parameter Type</param>
        /// <param name="size">Parameter Size</param>




        public object getValue(string name)
        {
            object parameter = null;
            try
            {
                parameter = cmd.Parameters[name].Value;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return parameter;
        }

        public void ExecuteNoneQuery(string commandText, CommandType commandType)
        {
            try
            {
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                setconnection();
                cmd.Connection = conn;

                //cmd.Parameters.Add("@RETVAL", SqlDbType.BigInt).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                //if (cmd.Parameters["@RETVAL"].Value.ToString() != "0" || cmd.Parameters["@RETVAL"].Value.ToString() != string.Empty)
                //{
                //    return Convert.ToUInt64(cmd.Parameters["@RETVAL"].Value.ToString());
                //}
                //else
                //{
                //    return 0;
                //}
            }
            catch (SqlException ex)
            {
                RollbackTransaction();
                if (ex.Number == 1205 || ex.Message.ToLower().Contains("was deadlocked on lock resources") || ex.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    //System.Threading.Thread.Sleep(1000);
                    ExecuteNoneQuery(commandText, commandType);
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex1)
            {
                RollbackTransaction();
                if (!ex1.Message.ToLower().Contains("was deadlocked on lock resources") && ex1.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    //System.Threading.Thread.Sleep(1000);
                    ExecuteNoneQuery(commandText, commandType);
                }
                else
                {
                    throw ex1;
                }
            }
            finally
            {
                closeConnection();
            }
        }

        public IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            try
            {
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                setconnection();
                cmd.Connection = conn;
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                RollbackTransaction();
                if (ex.Number == 1205 || ex.Message.ToLower().Contains("was deadlocked on lock resources") || ex.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    return ExecuteReader(commandText, commandType);
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex1)
            {
                RollbackTransaction();
                if (!ex1.Message.ToLower().Contains("was deadlocked on lock resources") && ex1.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    //System.Threading.Thread.Sleep(1000);
                    return ExecuteReader(commandText, commandType);
                }
                else
                {
                    throw ex1;
                }
            }
            finally
            {
                //closeConnection();
            }
        }


        /// <summary>
        /// starts Transaction
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    cmd.Transaction = trans;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Commits Transaction
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (trans != null)
                {
                    trans.Commit();
                    conn.Close();
                    trans = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                closeConnection();
            }

        }

        /// <summary>
        /// RollBacks Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (trans != null)
                {
                    trans.Rollback();
                    conn.Close();
                    trans = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                closeConnection();
            }

        }
        public static class JsonUtils
        {
            public static List<Dictionary<String, Object>> ConvertDataTableToJsonString(DataTable dataTable)
            {

                List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

                Dictionary<String, Object> row;

                foreach (DataRow dr in dataTable.Rows)
                {
                    row = new Dictionary<String, Object>();
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        if (Convert.ToString(col.DataType.Name) == "Byte[]")
                        {
                            if (dr[col] != DBNull.Value)
                            {
                                try
                                {
                                    row.Add(col.ColumnName, "data:image;base64," + Convert.ToBase64String((byte[])dr[col]));
                                }
                                catch (Exception ex)
                                {
                                    row.Add(col.ColumnName, ex.Message);
                                }
                            }
                            else
                            {
                                row.Add(col.ColumnName, "");
                            }
                        }
                        else if (Convert.ToString(col.DataType.Name).ToLower() == "datetime" || Convert.ToString(col.DataType.Name).ToLower() == "date")
                        {
                            if (dr[col] != DBNull.Value)
                            {
                                try
                                {
                                    row.Add(col.ColumnName, Convert.ToDateTime(dr[col]).ToString("dd/MM/yyyy"));
                                }
                                catch (Exception ex)
                                {
                                    row.Add(col.ColumnName, ex.Message);
                                }
                            }
                            else
                            {
                                row.Add(col.ColumnName, "");
                            }
                        }
                        else
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                    }
                    tableRows.Add(row);
                }
                return (tableRows);
            }
        }


    }
}