using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyPlatform.Common.Cache
{
    public static class DBInfoCache
    {
        public static object GetDBInfo()
        {
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            if (DBList == null)
            {
                XMLHelper xmlHelper = new XMLHelper();
                xmlHelper.FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/DataBase.xml");
                List<Dictionary<string, string>> li = new List<Dictionary<string, string>>();
                XmlNodeList nodeList = xmlHelper.GetNodeList("/root/DB");
                foreach (XmlNode node in nodeList)
                {
                    if (node.HasChildNodes)
                    {
                        Dictionary<string, string> dicDB = new Dictionary<string, string>();
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            dicDB.Add(item.Name, item.InnerText);
                        }
                        li.Add(dicDB);
                    }
                    else
                    {
                        throw new Exception("数据库信息配置不正确");
                    }
                }
                cache.SetCache("Sys_DBList", li);                
            }
            return DBList;
        }
    }
}
