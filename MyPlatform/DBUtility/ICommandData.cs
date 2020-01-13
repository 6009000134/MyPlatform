using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public interface ICommandData
    {

        string CommandText { get; set; }
        SqlConnection connection { get; set; }
        SqlCommand cmd { get; set; }

    }
    public enum CommandType
    {
        
    }
}
