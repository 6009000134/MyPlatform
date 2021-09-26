﻿using MyPlatform.DBUtility;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.IDAL
{
    public interface IQueryObject
    {
        QueryObject GetQueryObjectData(IDataBase db,int id);
        DataSet GetQueryList(IDataBase db, MyPlatform.Model.QueryObject objectInfo);
    }
}