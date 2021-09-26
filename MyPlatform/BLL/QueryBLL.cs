using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.BLL
{
    public class QueryBLL
    {
        IQueryObject dal = DALFactory.DataAccess.CreateInstance<IQueryObject>("QueryObject");
        public DataSet GetList(QueryObject o)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取查询对象信息
                IDataBase db = DBUtility.DBHelperFactory.Create("Default");
                //获取查询结果
                ds = GetQueryList(o);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public QueryObject GetQueryObjectData(int id)
        {
            QueryObject o;
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create("Default");
                o = dal.GetQueryObjectData(db, id);
                if (o == null)
                {
                    throw new Exception("查询视图不存在");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return o;
        }
        private DataSet GetQueryList(QueryObject o)
        {
            //查询数据
            DataSet ds = new DataSet();
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create(o.TableInfo.DBCon);
                ds = dal.GetQueryList(db, o);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
