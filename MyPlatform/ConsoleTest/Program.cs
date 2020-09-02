using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using JWT.Serializers;
using MyPlatform.Common;
using MyPlatform.DBUtility;
using MyPlatform.SQLServerDAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UFIDA.U9.CBO.PubBE.YYC;
using MySql.Data.MySqlClient;


namespace ConsoleTest
{

    class Program
    {
        static void Main(string[] args)
        {
            //DateTime startDate = new DateTime(2010,05,01);
            //double amount = 60.00;
            //for (int i = 0; i < 30; i++)
            //{
            //    amount += amount * 0.08+10;
            //    Console.WriteLine(amount);
            //}
            //// AddDailyBasic(startDate);       
            AddBonus("601398.SH");
            Console.ReadLine();
        }

        public static void AddBonus(string ts_code)
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "dividend");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("ts_code", ts_code);
            dicParam.Add("ann_date", "");
            dicParam.Add("record_date", "");
            dicParam.Add("ex_date", "");
            dicParam.Add("imp_ann_date", "");
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,end_date,ann_date,div_proc,stk_div,stk_bo_rate,stk_co_rate,cash_div,cash_div_tax,record_date,ex_date,pay_date,div_listdate,imp_ann_date,base_date,base_share");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_Bonus values('"+ts_code+"','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ",'" + ri.data.items[i][11] + "'";
                sql += ",'" + ri.data.items[i][12] + "'";
                sql += ",'" + ri.data.items[i][13] + "'";
                sql += ",'" + ri.data.items[i][14] + "'";
                sql += ",'" + ri.data.items[i][15] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
        }

        public static void AddDaily(DateTime startDate)
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "index_daily");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("trade_date", "");
            dicParam.Add("ts_code", "399006.SZ");
            dicParam.Add("start_date", startDate.ToString("yyyyMMdd"));
            dicParam.Add("end_date", startDate.AddDays(3000).ToString("yyyyMMdd"));
            startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,trade_date,close,open,high,low,pre_close,change,pct_chg,vol,amount");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex_Daily values('399006.SZ','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
            if (startDate<DateTime.Now)
            {
                AddDaily(startDate.AddDays(1));
            }
        }
        public static void AddDailyBasic(DateTime startDate)
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "index_dailybasic");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("trade_date", "");
            dicParam.Add("ts_code", "399006.SZ");
            dicParam.Add("start_date", startDate.ToString("yyyyMMdd"));
            dicParam.Add("end_date", startDate.AddDays(3000).ToString("yyyyMMdd"));
            startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,trade_date,total_mv,float_mv,total_share,float_share,free_share,turnover_rate,turnover_rate_f,pe,pe_ttm,pb");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex values('399006.SZ','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ",'" + ri.data.items[i][11] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
            if (startDate < DateTime.Now)
            {
                AddDailyBasic(startDate.AddDays(1));
            }
        }

    }

    class ResultInfo
    {
        public string request_id { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public Data2 data { get; set; }
        public bool has_more { get; set; }

    }
    class Data2
    {
        public string[] fields { get; set; }
        public List<string[]> items { get; set; }

    }
}
