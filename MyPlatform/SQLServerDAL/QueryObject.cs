using MyPlatform.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlatform.Model;
using MyPlatform.DBUtility;
using System.Data;

namespace MyPlatform.SQLServerDAL
{
    public class QueryObject : IQueryObject
    {
        /// <summary>
        /// 获取查询对象字段信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.QueryObject GetQueryObjectData(IDataBase db, int id)
        {
            MyPlatform.Model.QueryObject o = new Model.QueryObject();
            try
            {
                DataSet ds = new DataSet();
                string sql = string.Format("select * from sys_tables where id={0}", id);
                string sql2 = string.Format("select * from sys_columns where tableid={0}", id);
                List<string> liSqls = new List<string>();
                liSqls.Add(sql);
                liSqls.Add(sql2);
                ds = db.Query(liSqls);
                if (ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    o.TableInfo = new Model.Sys_Tables();
                    o.ColumnInfo = new List<Model.Sys_Columns>();
                    //表信息
                    o.TableInfo.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                    o.TableInfo.DBCon = ds.Tables[0].Rows[0]["DBCon"].ToString();                    
                    //列信息
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        MyPlatform.Model.Sys_Columns col = new Model.Sys_Columns();
                        col.ColumnName = ds.Tables[1].Rows[i]["ColumnName"].ToString();
                        col.ColumnName_CN = ds.Tables[1].Rows[i]["ColumnName"].ToString();
                        col.ColumnName_EN = ds.Tables[1].Rows[i]["ColumnName"].ToString();
                        col.ColumnType = ds.Tables[1].Rows[i]["ColumnName"].ToString();
                        col.CreatedBy = ds.Tables[1].Rows[i]["CreatedBy"].ToString();
                        col.CreatedDate = Convert.ToDateTime(ds.Tables[1].Rows[i]["CreatedDate"].ToString());
                        col.IsNullable = Convert.ToBoolean(ds.Tables[1].Rows[i]["IsNullable"]);
                        col.OrderNo = Convert.ToInt32(ds.Tables[1].Rows[i]["OrderNo"]);
                        col.Size =Convert.ToInt32(ds.Tables[1].Rows[i]["Size"]);
                        o.ColumnInfo.Add(col);
                    }
                }
                else
                {
                    throw new Exception("查不到查询对象信息");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return o;
        }
        /// <summary>
        /// 获取查询列表,TODO:分页
        /// </summary>
        /// <param name="db"></param>
        /// <param name="objectInfo"></param>
        /// <returns></returns>
        public DataSet GetQueryList(IDataBase db, MyPlatform.Model.QueryObject objectInfo)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "";
                switch (db.DBType)
                {
                    case Model.Enum.DBEnum.SqlServer:
                        sql = "select ";
                        for (int i = 0; i < objectInfo.ColumnInfo.Count; i++)
                        {
                            sql += objectInfo.ColumnInfo[i].ColumnName + ",";
                        }
                        sql = sql.TrimEnd(',') + " from " + objectInfo.TableInfo.TableName;
                        break;
                    case Model.Enum.DBEnum.MySql:
                        break;
                    case Model.Enum.DBEnum.Oracle:
                        break;
                    default:
                        break;
                }
                ds = db.Query(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
