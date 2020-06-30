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

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTimeOffset.Now.ToUniversalTime());
            string token=GetToken();
            Console.WriteLine(token);
            string t = Console.ReadLine();

            ValidateTOken(token);
            Console.ReadLine();
            //eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE1OTM1MDgyODAsImFjY291bnQiOiJsaXVmZWkifQ.pP6PZ4l_X7YcIY0ILOHg5g2I-ZHEuPskSmC6dSywUwk

        }
        private static string GetToken()
        {
            JWT.Builder.JwtBuilder b = new JWT.Builder.JwtBuilder();
            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            var token = new JwtBuilder()
        .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
        .WithSecret(secret)
        .AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(5).ToUnixTimeSeconds())
        .AddClaim("account", "liufei")
        .Encode();
            return token;
        }
        private static void ValidateTOken(string t)
        {
            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            string token = t; //"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjMsImFjY291bnQiOiJsaXVmZWkifQ.6pJoSEsRUMxwMXWUkW4ID7Y8pEbefa-LgoajvvMheds";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, secret, verify: true);
                Console.WriteLine(json);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
        }

        private void GetHolidays()
        {
            string data = HttpMethod.PostMethod("https://www.mxnzp.com/api/holiday/list/year/2020?app_id=akotsopmynpyvepi&app_secret=bUc0YThNcXZZUTVCVjN0OFp0Z1UyUT09", "");
            data1 dd = JSONUtil.ParseFromJson<data1>(data);
            List<string> sqls = new List<string>();
            List<string> dates = new List<string>();
            List<string> dates2 = new List<string>();
            dates2.Add("2020-06-13");
            dates2.Add("2020-06-14");
            if (dd.data.Count > 0)
            {
                for (int i = 0; i < dd.data.Count; i++)
                {
                    if (dd.data[i].days.Count > 0)
                    {
                        for (int j = 0; j < dd.data[i].days.Count; j++)
                        {
                            if (dd.data[i].days[j].type > 0)
                            {
                                dates.Add(dd.data[i].days[j].date);
                            }
                        }
                    }
                }
            }
            if (dates.Count > 0)
            {
                for (int i = 0; i < dates.Count; i++)
                {
                    string sql = "insert into auctus_temp values ('" + dates[i] + "');";
                    sqls.Add(sql);
                }
            }
            List<string> r = dates.Where(a => !dates2.Exists(t => t == a)).ToList();
            List<string> rs = dates.Where(a => a == "2020-06-13").ToList();

            //SqlConnection con = new SqlConnection("Data Source=192.168.1.81;Initial Catalog=AuctusERP;User ID=sa;Password=db@auctus998.;");
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //con.Open();
            //for (int i = 0; i < sqls.Count; i++)
            //{
            //    cmd.CommandText = sqls[i];
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();
            //}
            //con.Close();
            Console.ReadLine();
        }

        private static Hashtable GenPostData2()
        {
            Hashtable dic = new Hashtable();
            dic.Add("api_name", "dividend");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            dic.Add("params", GenParams2());
            dic.Add("fields", "ts_code,end_date,ann_date,div_proc,stk_div,stk_bo_rate,stk_co_rate,cash_div,cash_div_tax,record_date,ex_date,pay_date,div_listdate,imp_ann_date,base_date,base_share");
            return dic;
        }
        private static Dictionary<string, string> GenPostData()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("api_name","dividend");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            dic.Add("params",GenParams());
            dic.Add("fields", "ts_code,end_date,ann_date,div_proc,stk_div,stk_bo_rate,stk_co_rate,cash_div,cash_div_tax,record_date,ex_date,pay_date,div_listdate,imp_ann_date,base_date,base_share");
            return dic;
        }
        private static string GenParams()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ts_code","600900.sh");
            return Newtonsoft.Json.JsonConvert.SerializeObject(dic);
        }

        private static Dictionary<string,string> GenParams2()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ts_code", "600900.sh");
            return dic;
        }
        private void Cal()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("请输入总金额：");
                string total = Console.ReadLine();
                double dTotal;
                if (!Double.TryParse(total, out dTotal))
                {
                    Console.WriteLine("请输入总金额：");
                }
                Console.WriteLine("请输入每年增量金额：");
                string increaseAmount = Console.ReadLine();
                double amount;
                if (!Double.TryParse(increaseAmount, out amount))
                {
                    Console.WriteLine("请输入每年增量金额：");
                }
                Console.WriteLine("请输入增长率：");
                string rate = Console.ReadLine();
                double dRate;
                if (!Double.TryParse(rate, out dRate))
                {
                    Console.WriteLine("请输入每年增量金额：");
                }
                for (int i = 0; i < 15; i++)
                {
                    dTotal = dTotal * (1 + dRate) + amount;
                    Console.WriteLine("第" + (i + 1).ToString() + "年:" + dTotal);
                }
                Console.WriteLine("输入0结束，否则继续！");
                string result = Console.ReadLine();
                if (result == "0")
                {
                    flag = false;
                }
            }
        }
        #region 测试DAL
        private static void Add()
        {
            Sys_Tables dal = new Sys_Tables();
            MyPlatform.Model.Sys_Tables model = new MyPlatform.Model.Sys_Tables();
            model.CreatedBy = "1";
            model.DBName = "aa";
            dal.Add(model);
        }
        #endregion

        #region 辅助方法
        private static string CreateInsertSqlByRef<T>(T t)
        {
            PropertyInfo[] pis = t.GetType().GetProperties();
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert Into " + t.GetType().Name);
            sql.Append(" (");
            foreach (PropertyInfo pi in pis)
            {
                sql.Append(pi.Name + ",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            sql.Append(" values (");
            foreach (PropertyInfo pi in pis)
            {
                sql.Append(" @" + pi.Name + ",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            SqlParameter[] paras = new SqlParameter[pis.Length];
            return sql.ToString();
        }
        #endregion

    }
    class data1 {
        public string code { get; set; }
        public string msg { get; set; }
        public List<data2> data { get; set; }
    }
    class data2 {
        public string month { get; set; }

        public string year { get; set; }
        public List<data3> days { get; set; }
    }
    class data3 {
        public string date { get; set; }
        public int type { get; set; }

    }
    class Doc
    {
        public string DocNo { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Lines> lines { get; set; }

    }
    public class Lines
    {
        public int LineNum { get; set;  }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
