using MyPlatform.DBUtility;
using MyPlatform.Model;
using MyPlatform.Model.Chart;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.BLL
{
    public class BLL4OAChart
    {

        public ReturnData GetData(string xmbm, string strTypes)
        {
            ReturnData result = new ReturnData();
            IDataBase db = DBHelperFactory.CreateDBInstance("OACon");
            string[] arr = strTypes.Split(',');
            List<EChartModel> list = new List<EChartModel>();
            for (int i = 0; i < arr.Length; i++)
            {
                switch (arr[i])
                {
                    //case "Bug状态":                        
                    //case "发生阶段":
                    //case "故障大类":
                    //case "故障小类":
                    //case "严重程度":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                        list.Add(GetSeries(db, xmbm, arr[i], "1"));
                        break;
                    case "":
                        break;
                    default:
                        break;
                }
            }
            result.D = list;
            return result;
        }
        public List<EChartModel> GetECharts()
        {
            List<EChartModel> li = new List<EChartModel>();
            return li;
        }

        public EChartModel GetSeries(IDataBase db, string xmbm, string type, string sfsx)
        {
            EChartModel model = new EChartModel();
            string domid = "main" + type;
            model.DomID = domid;
            //title
            JObject title = new JObject();
            JObject series = new JObject();
            //model.xAxis
            //model.yAxis
            //model.grid
            string sql = "";
            switch (type)
            {
                case "1":
                    title.Add("text", "Bug状态");
                    series.Add("type", "pie");
                    series.Add("radius", "15%");
                    series.Add("datasetIndex", 0);
                    JObject encode = new JObject();
                    encode.Add("itemName", "Bug状态");
                    encode.Add("value", "数量");
                    series.Add("encode", encode);
                    JObject format = new JObject();
                    format.Add("formatter", "{b}|{@[1]}个{d}%");
                    series.Add("label", format);

                    model.title.Add(title);
                    model.series.Add(series);
                    sql = @"select 'Bug状态' Item,'数量' Quantity from dual
union all
select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a
left join
(
select a.xmmc, a.xmbm, a.status, count(a.Status)Quantity, '1'Type, 'Bug状态'TypeName from v_bugfreeinfo a
where a.nodeorder > 0
                                                                                   group by a.xmmc, a.xmbm, a.Status
) b on a.xmbm = b.xmbm and a.type = b.type and a.item = b.status
where a.type = '1'and a.xmbm = '" + xmbm + "'";
                    break;
                case "2":
                    break;
                default:
                    break;
            }
            if (sfsx == "0")
            {
                sql += " sfsx=0";
            }
            DataSet ds = db.Query(sql);
            List<Dictionary<string, object>> dataset = new List<Dictionary<string, object>>();
            List<List<string>> liData = new List<List<string>>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JObject t = new JObject();
                List<string> temp = new List<string>();
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    temp.Add(ds.Tables[0].Rows[i][j].ToString());
                }
                liData.Add(temp);
                Dictionary<string, object> dicTemp = new Dictionary<string, object>();
                dicTemp.Add("source", liData);
                model.dataset.Add(dicTemp);
            }

            return model;
        }
    }
}
