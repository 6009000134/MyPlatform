using System;
using System.Data;
using MyPlatform.Model;
namespace MyPlatform.IDAL
{
    /// <summary>
    /// 接口层Sys_Users
    /// </summary>
    public interface ISys_Users
    {
        #region Extend by liufei
        /// <summary>
        /// 验证账号是否注册
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        bool Exists(string Account);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Exists(Sys_Users model);
        /// <summary>
        /// 通过账号获取账号信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        Sys_Users GetModelByAccount(string Account);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(MyPlatform.Model.Sys_Users model);
        #endregion
    }
}