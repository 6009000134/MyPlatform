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
        public Model.QueryObject GetQueryObject(IDataBase db, int id)
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
        public DataSet GetQueryResult(IDataBase db, MyPlatform.Model.QueryObject objectInfo)
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
