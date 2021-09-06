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
        public DataSet Query(int id)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取查询对象信息
                IDataBase db = DBUtility.DBHelperFactory.Create("Default");
                QueryObject o = dal.GetQueryObject(db,id);
                if (o==null)
                {
                    throw new Exception("不存在查询对象信息");
                }
                //获取查询结果
                ds = GetQueryResult(o);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return ds;
        }
        
        private DataSet GetQueryResult(QueryObject o)
        {
            //查询数据
            DataSet ds = new DataSet();
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create(o.TableInfo.DBCon);
                ds = dal.GetQueryResult(db, o);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
