using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model.Query
{
    /// <summary>
    /// 查询页面
    /// </summary>
    public class QueryView
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 类型（表/视图/存储过程）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 名称（表名/视图名/存储过程名）
        /// </summary>
        public string Name { get; set; }
    }
    public class QueryViewDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// ViewID
        /// </summary>
        public string ViewID { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string Type { get; set; }
    }
    public class SqlServerDBType
    {
        /// <summary>
        /// ColID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// ColID
        /// </summary>
        public string ColID { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public int Precision { get; set; }
    }

}
