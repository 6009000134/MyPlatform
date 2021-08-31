﻿using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
using MyPlatform.DBUtility;

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
                //根据tableid获取数据库连接名
                Sys_Tables t = new Sys_Tables();
                ReturnData tableInfo = t.GetDetail(model.TableID, new Pagination() { PageSize = 10, PageIndex = 1 });
                DataSet ds = (DataSet)tableInfo.D;
                string str = ds.Tables[0].Rows[0].ToJson();
                MyPlatform.Model.Sys_Tables table = ModelConverter<MyPlatform.Model.Sys_Tables>.ConvertToModelEntity(ds.Tables[0]);
                model.TableName = table.TableName;
                IDataBase db = DBHelperFactory.Create(table.DBCon);
                result=dal.Add(db,model);
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