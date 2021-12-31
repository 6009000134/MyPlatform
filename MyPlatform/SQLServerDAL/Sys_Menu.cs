﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
namespace MyPlatform.SQLServerDAL
{
    //Sys_Menu
    public partial class Sys_Menu : ISys_Menu
    {

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Add(MyPlatform.Model.Sys_Menu model, IDataBase db)
        {
            List<SqlCommandData> liSql = new List<SqlCommandData>();
            SqlCommandData scdMenu = new SqlCommandData();
            string sql = @"INSERT INTO dbo.Sys_Menu
        ( CreatedBy ,
          CreatedDate ,
          UpdatedBy ,
          UpdatedDate ,
          MenuName ,
          Uri ,
          ParentID
        )
VALUES  ( @CreatedBy , -- CreatedBy - nvarchar(20)
          GETDATE() , -- CreatedDate - datetime
          @UpdatedBy , -- UpdatedBy - nvarchar(20)
          GETDATE() , -- UpdatedDate - datetime
          @MenuName , -- MenuName - nvarchar(50)
          @Uri , -- Uri - varchar(300)
          @ParentID -- ParentID - int
        )";
            List<SqlParameter> liMenuParas = new List<SqlParameter> {
                new SqlParameter("@CreatedBy",model.CreatedBy),
                new SqlParameter("@UpdatedBy",model.UpdatedBy),
                new SqlParameter("@MenuName",model.MenuName),
                new SqlParameter("@Uri",model.Uri),
                new SqlParameter("@ParentID",model.ParentID)
            };
            scdMenu.CommandBehavior = SqlServerCommandBehavior.ExecuteNonQuery;
            scdMenu.CommandText = sql;
            scdMenu.Paras.AddRange(liMenuParas);

            string sqlRouter = @"	INSERT INTO dbo.Sys_VueRouter
		        ( Path, Name, Meta, Component, MenuID )
		VALUES  ( @Path, -- Path - varchar(200)
		          @Name, -- Name - varchar(200)
		         @Meta, -- Meta - nvarchar(1000)
		          @Component, -- Component - varchar(500)
		          (select IDENT_CURRENT('sys_menu'))  -- MenuID - int
		          )";
            List<SqlParameter> liRouterParas = new List<SqlParameter> {
                new SqlParameter("@Path",model.Router.Path),
                new SqlParameter("@Name",model.Router.Name),
                new SqlParameter("@Meta",model.Router.Meta),
                new SqlParameter("@Component",model.Router.Component)
            };
            SqlCommandData scdRouuter = new SqlCommandData();
            scdRouuter.CommandBehavior = SqlServerCommandBehavior.ExecuteNonQuery;
            scdRouuter.CommandText = sqlRouter;
            scdRouuter.Paras.AddRange(liRouterParas);
            liSql.Add(scdMenu);
            liSql.Add(scdRouuter);
            return db.ExecuteTran(liSql);
        }
        public DataSet GetMenuTree(IDataBase db)
        {
            string sql = @"
WITH menu(ID,MenuName,ParentID,FullMenuPath) AS
(
SELECT a.ID,a.MenuName,a.ParentID,CAST(a.MenuName  AS VARCHAR(8000))
FROM dbo.Sys_Menu a WHERE a.ParentID=0
UNION ALL 
SELECT b.ID,b.MenuName,b.ParentID,cast (CONVERT(varchar(100),a.FullMenuPath)+'/'+CONVERT(VARCHAR(100),b.MenuName) AS varchar(8000))--,CONVERT(NVARCHAR(MAX),a.FullMenuPath+b.MenuName)
FROM menu a INNER JOIN dbo.Sys_Menu b ON a.ID=b.ParentID
)
SELECT * FROM menu
order by FullMenuPath
";
            return db.Query(sql);
        }
    }
}

