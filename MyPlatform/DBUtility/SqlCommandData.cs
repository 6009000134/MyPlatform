using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public class SqlCommandData
    {
        /// <summary>
        /// sql命令
        /// </summary>
        public string CommandText
        {
            get; set;
        }
        /// <summary>
        /// 超时时间
        /// </summary>
        public int CommandTimeout { get; set; }
        /// <summary>
        /// 参数集合
        /// </summary>
        public SqlParameter[] Paras { get; set; }
        /// <summary>
        /// 操作类型 ExecuteNonQuery\ExecuteSclar\ExecuteReader
        /// </summary>
        public SqlServerCommandBehavior CommandBehavior { get; set; }
        private CommandType _commandType;
        /// <summary>
        /// sql命令类型 Text\Procedure\TableDirect
        /// </summary>
        public CommandType CommandType
        {
            get { return _commandType; }
            set
            {
                if (value == 0)
                {
                    value = CommandType.Text;
                }
                else
                {
                    _commandType = value;
                }
            }
        }
    }
}
