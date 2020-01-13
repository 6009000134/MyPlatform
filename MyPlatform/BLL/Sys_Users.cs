using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
using MyPlatform.DBUtility;

namespace MyPlatform.BLL
{
    public class Sys_Users
    {
        private readonly ISys_Users dal = DataAccess.CreateInstance<ISys_Users>("Sys_Users");
        //private readonly ISys_Users dal = DataAccess.CreateSysUsers();

        public MyPlatform.Model.Sys_Users GetModelByAccount(string account)
        {            
            return dal.GetModelByAccount(account);
        }
        public bool Exists(MyPlatform.Model.Sys_Users model)
        {
            return dal.Exists(model);
        }
        public bool Exists(string account)
        {
            return dal.Exists(account);
        }
        public int Add(MyPlatform.Model.Sys_Users model)
        {
            return dal.Add(model);
        }
    }
}
