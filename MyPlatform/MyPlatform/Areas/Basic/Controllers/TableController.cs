﻿using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Basic.Controllers
{
    /// <summary>
    /// 表操作
    /// </summary>
    [AllowAnonymous]
    public class TableController : ApiController
    {
        MyPlatform.BLL.Sys_Tables tableBLL = new MyPlatform.BLL.Sys_Tables();
  
        /// <summary>
        /// 创建系统表，默认创建ID、Deleted、CreateBy、CreateDate、UpdateBy、UpdateDate字段
        /// </summary>
        /// <param name="model">表信息</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Add(Sys_Tables model)
        {
            ReturnData result = new ReturnData();
            try
            {
                //校验是否存在同名表
                result = tableBLL.ExistsTable(model.TableName, model.DBCon);
                if (result.S)
                {
                    if (model.CreatedDate == null)
                    {
                        model.CreatedDate = DateTime.Now;
                    }
                    //创建表
                    result = tableBLL.Add(model);
                }
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        /// <summary>
        /// 编辑表信息
        /// </summary>
        /// <param name="model">表信息</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Edit(Sys_Tables model)
        {
            ReturnData result = new ReturnData();
            try
            {
                if (model.UpdatedDate == null)
                {
                    model.UpdatedDate = DateTime.Now;
                }
                if (tableBLL.Edit(model))
                {
                    result.SetErrorMsg("修改失败！");
                }
                else
                {
                    result.SetSuccessMsg("修改成功！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Default.WriteError("修改表信息失败：" + ex.Message);
                result.SetErrorMsg("修改表信息失败：" + ex.Message);
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        /// <summary>
        /// 删除表信息数据，但是不删除数据库中具体表，需要手动去数据库删
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Delete(int tableID)
        {
            ReturnData result = new ReturnData();
            if (tableBLL.Delete(tableID))
            {
                // Request.Content.IsMimeMultipartContent
                // MultipartFileStreamProvider
                // System.Web.HttpContext.Current.Request.Files[0]
                // Request.Content.ReadAsMultipartAsync()
                result.S = true;
            }
            else
            {
                result.SetErrorMsg("删除表失败");
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        /// <summary>
        /// ss
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage List([FromUri]List<QueryParam> qp)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.D = tableBLL.GetListByDBName("");
                result.S = true;
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("获取数据失败：" + ex.Message);
                LogHelper.Default.WriteError("获取数据失败：" + ex.Message);
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        [HttpGet]
        public HttpResponseMessage GetDetail(Dictionary<string, object> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                //Pagination page=JSONUtil.ParseFromJson<>
                int tableID = Convert.ToInt32(dic["tableID"]);
                string s = dic["page"].ToJson();
                Pagination page=JSONUtil.ParseFromJson<Pagination>(dic["page"].ToJson());
                BLL.Sys_Columns columnBLL = new BLL.Sys_Columns();
                result = tableBLL.GetDetail(Convert.ToInt32(dic["tableID"]),page);
            }
            catch (Exception ex)
            {
                //result.S = false;
                //result.SetErrorMsg("错误信息：" + ex.Message);
                throw ex;                
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        /// <summary>
        /// 查询数据库表信息
        /// </summary>
        /// <param name="dbCon">数据库名</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetSysTableList(string dbCon)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.S = true;
                result.D = tableBLL.GetSysTableList(dbCon).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        /// <summary>
        /// 同步表信息
        /// </summary>
        /// <param name="dbCon"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SyncTableInfo([FromBody]Dictionary<string,string> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                string dbCon="";
                string tableName="";
                foreach (string key in dic.Keys)
                {
                    switch (key)
                    {
                        case "dbCon":
                            dbCon = dic["dbCon"];
                            break;
                        case "tableName":
                            tableName = dic["tableName"];
                            break;
                        default:
                            break;
                    }
                }
                if (string.IsNullOrEmpty(dbCon) || string.IsNullOrEmpty(tableName))
                {
                    result.SetErrorMsg("参数名不对！");
                }
                else
                {
                    result = tableBLL.SyncTableInfo(dbCon, tableName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

    }
}
