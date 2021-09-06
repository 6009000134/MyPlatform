using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// 查询对象
    /// </summary>
    public class QueryObject
    {
        /// <summary>
        /// 对象名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 查询对象
        /// </summary>
        public QueryObject()
        {
            PageInfo = new Pagination {PageSize=10,PageIndex=1 };
        }
        /// <summary>
        /// 表信息
        /// </summary>
        public MyPlatform.Model.Sys_Tables TableInfo { get; set; }
        /// <summary>
        /// 列信息
        /// </summary>
        public List<MyPlatform.Model.Sys_Columns> ColumnInfo { get; set; }
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pagination PageInfo { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<List<QueryCondition>> Condition { get; set; }
    }
}
