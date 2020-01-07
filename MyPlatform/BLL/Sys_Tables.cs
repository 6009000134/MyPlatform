using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_Tables
		public partial class Sys_Tables
	{
		private readonly ISys_Tables dal=DataAccess.CreateInstance<ISys_Tables>("Sys_Tables");
		public Sys_Tables()
		{}
        #region extend 
        public string Test()
        {
            return dal.Test();
        }
        public DataTable GetListByDBName(string DBName)
        {
            return dal.GetListByDBName(DBName);
        }        
        #endregion
	}
}