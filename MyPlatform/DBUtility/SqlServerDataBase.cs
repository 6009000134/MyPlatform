using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public class SqlServerDataBase : IDataBase
    {
        //TODO:Timeout设置
        public string connectionString;//连接字符串
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlServerDataBase()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbname">数据库连接名</param>
        public SqlServerDataBase(string dbname)
        {
            connectionString = ConfigurationManager.ConnectionStrings[dbname].ConnectionString;
        }
        #endregion

        #region 辅助方法
        public SqlCommand CreateCommand(string sql, IDataParameter[] paras)
        {
            SqlCommand cmd = new SqlCommand(sql);
            foreach (SqlParameter item in paras)
            {
                if ((item.Direction == ParameterDirection.Input || item.Direction == ParameterDirection.InputOutput) && (item.Value == null))
                {
                    item.Value = DBNull.Value;
                }
                cmd.Parameters.Add(item);
            }
            return cmd;
        }
        #endregion

        #region 执行简单sql方法
        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 查询并返回SqlDataReader
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string sql)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql);
                    return cmd.ExecuteReader();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行查询语句，并返回第一行第一列数据
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql);
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 返回sql查询结果
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>返回DataSet</returns>
        public DataSet Query(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion

        #region 执行带参数sql方法
        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, IDataParameter[] paras)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, paras);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public object ExecuteScalar(string sql, IDataParameter[] paras)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, paras);
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行sql并返回查询结果集合
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public DataSet Query(string sql, IDataParameter[] paras)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, paras);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion
        #region 执行存储过程
        public DataSet ExecProcedure(string procedureName)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(procedureName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //cmd.ExecuteNonQuery();
            }
            return ds;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public DataSet ExecProcedure(string procedureName, IDataParameter[] paras)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(procedureName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter para in paras)
                    {
                        if ((para.Direction == ParameterDirection.Input || para.Direction == ParameterDirection.InputOutput) && para.Value == null)
                        {
                            para.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(para);
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //cmd.ExecuteNonQuery();
            }
            return ds;
        }
        #endregion
        #region 执行简单语句事务
        public bool ExecuteTran(List<string> liSql)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                bool flag = true;
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    try
                    {
                        int index = 0;
                        foreach (string sql in liSql)
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            index += 1;
                        }
                        tran.Commit();
                        flag = true;
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        flag = false;
                        throw ex;
                    }
                }
                return flag;
            }
        }
        #endregion
        #region 执行带参数事务
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="tranSqls">事务参数</param>
        /// <returns></returns>
        public bool ExecuteTran(List<SqlCommandData> tranSqls)
        {
            bool flag = true;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction ts = con.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.Transaction = ts;
                    try
                    {
                        foreach (SqlCommandData item in tranSqls)
                        {
                            cmd.CommandText = item.CommandText;
                            cmd.CommandType = item.CommandType;
                            if (item.Paras != null)
                            {
                                //SqlCommand参数赋值
                                foreach (SqlParameter para in item.Paras)
                                {
                                    if ((para.Direction == ParameterDirection.Input || para.Direction == ParameterDirection.InputOutput) && para.Value == null)
                                    {
                                        para.Value = DBNull.Value;
                                    }
                                    cmd.Parameters.Add(para);
                                }
                            }
                            if (item.CommandTimeout > 0)
                            {
                                cmd.CommandTimeout = item.CommandTimeout;
                            }
                            switch (item.CommandBehavior)
                            {
                                case SqlServerCommandBehavior.ExecuteNonQuery:
                                    cmd.ExecuteNonQuery();
                                    break;
                                case SqlServerCommandBehavior.ExecuteSclar:
                                    cmd.ExecuteScalar();
                                    break;
                                case SqlServerCommandBehavior.ExecuteReader:
                                    cmd.ExecuteReader();
                                    break;
                                default:
                                    cmd.ExecuteNonQuery();
                                    break;
                            }
                            cmd.Parameters.Clear();
                        }
                        ts.Commit();
                        flag = true;
                    }
                    catch (SqlException ex)
                    {
                        ts.Rollback();
                        flag = false;
                        throw ex;
                    }
                }
                con.Close();
            }
            return flag;
        }
        #endregion
    }
    public enum SqlServerCommandBehavior
    {
        ExecuteNonQuery,
        ExecuteSclar,
        ExecuteReader

    }
}
