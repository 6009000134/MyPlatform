using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL
{
    //Sys_VueRouter
    public partial class Sys_VueRouter
    {
        private readonly ISys_VueRouter dal = DataAccess.CreateInstance<ISys_VueRouter>("Sys_VueRouter");
        public Sys_VueRouter()
        { }


    }
}