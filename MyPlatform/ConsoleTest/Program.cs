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
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;

namespace ConsoleTest
{
    
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection con = new OracleConnection("user id=ecology;password=ecology;data source=192.168.20.29/orclpdb");
            con.Open();
            string sql = @"
declare v_sql varchar(1000);
begin
 v_sql:='select * from v_auctus_bugfreeinfo';
 execute immediate v_sql;
 end ;
";            
            OracleCommand cmd = new OracleCommand(sql,con);
            //cmd.ExecuteNonQuery();
            //OracleDataReader odr = cmd.ExecuteReader(CommandBehavior.Default);    
            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            con.Close();
            Console.ReadLine();
        }
        public static void Addfund()
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "fund_basic");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("market", "");
            dicParam.Add("status", "");
            //startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,name,management,custodian,fund_type,found_date,due_date,list_date,issue_date,delist_date,issue_amount,m_fee,c_fee,duration_year,p_value,min_amount,exp_return,benchmark,status,invest_type,type,trustee,purc_startdate,redm_startdate");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex values('600362.SH','" + ri.data.items[i][1] + "'";
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
            //if (startDate < DateTime.Now)
            //{
            //    AddDailyBasic(startDate.AddDays(1));
            //}
        }
        public static void AddDailyBasic2()
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "daily_basic");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("ts_code", "600362.SH");
            dicParam.Add("trade_date", "");
            dicParam.Add("start_date", "");
            dicParam.Add("end_date", "");
            //startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex values('600362.SH','" + ri.data.items[i][1] + "'";
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
            //if (startDate < DateTime.Now)
            //{
            //    AddDailyBasic(startDate.AddDays(1));
            //}
        }

        public static void AddAPIInfo(string html)
        {            
            List<string> li = new List<string>();//Sqls
            //标题
            Regex regTitle = new Regex("(?<=<h2.*>).*(?=</[\\s\\S]*?h2 >)");
            //API名称
            Regex regApi = new Regex("(?<=接口：).*?(?=<br>)");
            Regex regDescpri = new Regex("(?<=描述：).*?(?=<br>)");
            Match m = regTitle.Match(html);
            Match m1 = regApi.Match(html);
            Match m2 = regDescpri.Match(html);
            string title = m.Value;
            string api = m1.Value;
            string Descprition = m2.Value;
            Console.WriteLine("title:" + title);
            Console.WriteLine("api:" + api);
            Console.WriteLine("Descprition:" + Descprition);
            Console.WriteLine("如果有问题，请输入\"N\"终止");
            string sql = "insert into Sys_APIs values('" + m.Value + "','" + m1.Value + "','" + m2.Value + "')";
            string result = Console.ReadLine();
            if (result.ToUpper() == "N")
            {
                return;
            }
            sql += "; select SCOPE_IDENTITY()";
            IDataBase db = new SqlServerDataBase();
            object o = db.ExecuteScalar(sql);
            int apiID = int.Parse(o.ToString());
            Console.WriteLine("结果:" + o.ToString());


            //Input Html
            Match m3 = new Regex("输入参数[\\s\\S]*?输出参数").Match(html);
            string input = m3.Value;
            //输入参数            
            MatchCollection m4 = new Regex("(?<=<td.*>).*(?=</td>)").Matches(input);
            IEnumerator e = m4.GetEnumerator();
            int i = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("Insert into API_Input (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values(");
            while (e.MoveNext())
            {
                if (i % 4 == 0 && i != 0)
                {
                    li.Add(sb.ToString().Substring(0, sb.ToString().Length - 1) + "," + apiID.ToString() + "," + (i * 10).ToString() + ")");
                    sb = new StringBuilder();
                    sb.Append("Insert into API_Input (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values(");
                }
                sb.Append(" '" + e.Current.ToString() + "',");
                i++;
            }

            //输出参数Html
            Match m5 = new Regex("输出参数[\\s\\S]*?</table>").Match(html);
            string output = m5.Value;
            //输出参数
            MatchCollection m6 = new Regex("(?<=<td.*>).*(?=</td>)").Matches(output);
            IEnumerator e2 = m6.GetEnumerator();
            sb = new StringBuilder();
            sb.Append("Insert into API_OutPut (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values (");
            i = 0;
            while (e2.MoveNext())
            {
                if (i % 4 == 0 && i != 0)
                {
                    li.Add(sb.ToString().Substring(0, sb.ToString().Length - 1) + "," + apiID.ToString() + "," + (i * 10).ToString() + ")");
                    sb = new StringBuilder();
                    sb.Append("Insert into API_OutPut (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values (");
                }
                sb.Append(" '" + e2.Current.ToString() + "',");
                i++;
            }
            db.ExecuteTran(li);
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
                sql = "insert into Base_Bonus values('" + ts_code + "','" + ri.data.items[i][1] + "'";
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
            if (startDate < DateTime.Now)
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
            dicParam.Add("ts_code", "600362.SH");
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
                sql = "insert into Base_MarketIndex values('600362.SH','" + ri.data.items[i][1] + "'";
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
