using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
using MyPlatform.DBUtility;

namespace MyPlatform.BLL {
	 	//Sys_Menu
		public partial class Sys_Menu
	{
		private readonly ISys_Menu dal=DataAccess.CreateInstance<ISys_Menu>("Sys_Menu");
        private string defaultCon = "Default";
		public Sys_Menu()
        { }

        public bool Add(MyPlatform.Model.Sys_Menu model)
        {
            IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
            return dal.Add(model, db);
        }

        public ReturnData GetMenuTree()
        {
            ReturnData result = new ReturnData();
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
                DataSet ds = dal.GetMenuTree(db);
                result.S = true;
                result.D = ds;
            }
            catch (Exception ex)
            {
                throw ex;
                throw;
            }
            return result;
        }
   
	}
}