using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL
{
    //Sys_Columns
    public partial class Sys_Columns
    {
        private readonly ISys_Columns dal = DataAccess.CreateInstance<ISys_Columns>("Sys_Columns");
        public Sys_Columns()
        { }
        public ReturnData AddColumn(Model.Sys_Columns model)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.S = true;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DataSet GetList(int tableID)
        {
            return dal.GetList(tableID);
        }

    }
}