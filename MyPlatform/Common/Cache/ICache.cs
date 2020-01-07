using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPlatform.Common.Cache
{
    interface ICache
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetCache(string key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void SetCache(string key, object value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        void SetCache(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration);

    }
}
