using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace generatesignature
{

    class sms
    {
        //        "companyCode":"test",
        //"phone":"18952413202",
        //"templateId":"10104",
        //"msgType":0,
        //"vars":"{'code':'1001','minute':'2'}",
        //"source":0,
        //"signature":"57f120a612af8cd2384ebc7b14627911",
        //"timestamp":"1598424240259"
        public string companyCode { get; set; }
        public string phone { get; set; }
        public string msgType { get; set; }
        public string vars { get; set; }
        public string source { get; set; }
        public string signature { get; set; }
        public string timestamp { get; set; }
        public string templateId { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {

            //string11 = (expireDate?.Year * 1000 + expireDate?.DayOfYear - 1900000)
            #region 生成修改租户的数据
            // 生成修改租户的数据
            var file = File.ReadAllText("Draft.sql");
            var list = new List<string>();
            for (int i = 33; i <= 61; i++)
            {
                list.Add(file.Replace("scc_0710", "corehr_traincalculate" + i.ToString()).Replace("hrone6", "traincalculate" + i.ToString()));
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            // end
            #endregion


            // SortedDictionary<string,string>()
            StringBuilder msg = new StringBuilder();
            var pwd = args[0] ?? "123456";
            var argtenant = args[1] ?? "fabercastelltest";
            var strJ = args[2] ?? "{\"contract_End\":\"2023-06-30\",\"contract_Start\":\"2020-04-22\",\"contract_Type\":\"0\",\"employeeId\":\"002268\"}";
            String timeStamp = "1599445948";//((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + "";
            var nonce = "1599445948";// new Random().Next().ToString();
            msg.Append(strJ);
            msg.Append(Constant.TENANT).Append(argtenant).Append(Constant.TIMESTAMP).Append(timeStamp).Append(Constant.NONCE).Append(nonce);
            Console.WriteLine("pwd:" + pwd);
            Console.WriteLine("tenant:" + argtenant);
            Console.WriteLine("nonce:" + nonce);
            Console.WriteLine("timeStamp:" + timeStamp);
            //Console.WriteLine(msg);
            Console.WriteLine("sign:" + CreateToken(msg.ToString(), pwd));

            //var _sms = new sms();
            //_sms.companyCode = "test";
            //_sms.phone = "18952413202";
            //_sms.msgType = "0";
            //_sms.vars = "{\"code\":\"1001\",\"minute\":\"2\"}";
            //_sms.source = "0";
            //_sms.templateId = "10104";
            //_sms.timestamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000) + "";
            //var param_t = "companyCode=" + _sms.companyCode
            //                             + "&msgType=" + _sms.msgType
            //                             + "&phone=" + _sms.phone
            //                             + "&source=" + _sms.source
            //                             + "&templateId=" + _sms.templateId
            //                             + "&timestamp=" + _sms.timestamp
            //                             + "&vars=" + _sms.vars;

            //_sms.signature = MD5Str(param_t);
            //Console.WriteLine(param_t);
            //Console.WriteLine(_sms.signature.ToLower());
        }

        /// <summary>
        /// 这是最基础的MD5 加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Str(string password)
        {
            return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Default.GetBytes(password))).Replace("-", "");


        }


        private static string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return ByteTostrign(hashmessage);
            }
        }
        private static string ByteTostrign(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }

            return sb.ToString();

        }
    }

    public class jobj
    {
        public DateTime searchDate { get; set; }
    }

    public static class Constant
    {
        public static readonly String TENANT = "tenant";
        public static readonly String TIMESTAMP = "timestamp";
        public static readonly String NONCE = "nonce";
        public static readonly String CHARSET_UTF_8 = "utf-8";
    }
}

