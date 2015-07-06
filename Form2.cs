using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCompactProject.CControl;
using System.Net;
using System.IO;

namespace TestRF
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f1 = new Form1();
            f1.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("test");
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("pn", typeof(string));
            dt.Columns.Add("qty", typeof(string)); 

            dt.Columns.Add("cdesc", typeof(string));
            var dr = dt.NewRow();
            dr["id"] = "1";
            dr["name"] = "测试1";
            dr["pn"] = "111111";
            dr["qty"] = "1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "2";
            dr["name"] = "测2";
            dr["pn"] = "222222";
            dr["qty"] = "1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "3";
            dr["name"] = "测试3";
            dr["pn"] = "333333";
            dr["qty"] = "1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "4";
            dr["name"] = "测试4";
            dr["pn"] = "4444";
            dr["qty"] = "1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "5";
            dr["name"] = "测试5";
            dr["pn"] = "55555";
            dr["qty"] = "1";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "6";
            dr["name"] = "测试6";
            dr["pn"] = "66666";
            dr["qty"] = "1";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "7";
            dr["name"] = "测试7";
            dr["pn"] = "777777";
            dr["qty"] = "1";
            dt.Rows.Add(dr);

            foreach (DataRow d in dt.Rows)
            {
                var ui = new UserListItem(d["id"].ToString(), d["qty"].ToString(), d["pn"].ToString(), true);
                this.userDataList1.Add(ui);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //java调用
            string jurl = @"http://121.41.99.231:8088/admin/yubin/testhttp.do";

            string s = TestPost();// OpenReadWithHttps(jurl, "utf-8");
            MessageBox.Show(s);
            //string s1 = _GetHTML(jurl, "*/*", Encoding.Default, 1000);
            //MessageBox.Show(s1);

            ////My self 2015/06/24
            //HttpWebRequest hr = (HttpWebRequest)HttpWebRequest.Create(jurl);
            //hr.Method = "GET";
            //hr.Proxy = null;
            //HttpWebResponse hs = (HttpWebResponse)hr.GetResponse();
            //StreamReader sr = new StreamReader(hs.GetResponseStream());
            //string s2 = sr.ReadToEnd();
            //sr.Close();
            //MessageBox.Show(s2);
            ////end
        }
        public static string GetUrltoHtml(string Url, string type)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                //errorMsg = ex.Message;
            }
            return "";
        }
        ///<summary>
        ///采用https协议访问网络
        ///</summary>
        ///<param name="URL">url地址</param>
        ///<param name="strPostdata">发送的数据</param>
        ///<returns></returns>
        public string OpenReadWithHttps(string URL, string strPostdata, string strEncoding)
        {
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding)))
            {
                return reader.ReadToEnd();

            }
        }
        string TestPost()
        {
            try
            {
                string param = "testhttp.do";
                byte[] bs = Encoding.ASCII.GetBytes(param);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://121.41.99.231:8088/admin/yubin/testhttp.do");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bs.Length;
                req.Proxy = null;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
                using (WebResponse wr = req.GetResponse())
                {

                    StreamReader sr = new StreamReader(wr.GetResponseStream());
                    string result=  sr.ReadToEnd();
                    sr.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="URL">地址</param>
        /// <param name="Accept">Accept请求头</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="bufflen">数据包大小</param>
        /// <returns>Html代码</returns>
        private string _GetHTML(string URL, string Accept, Encoding encoding, int bufflen)
        {
            try
            {
                HttpWebRequest MyRequest = (HttpWebRequest)HttpWebRequest.Create(URL);
                MyRequest.Proxy = null;
                //MyRequest.Accept = Accept;
                //if (UserCookie == null)
                //{
                //    UserCookie = new CookieContainer();
                //}
                //MyRequest.CookieContainer = UserCookie;
                HttpWebResponse MyResponse = (HttpWebResponse)MyRequest.GetResponse();
                return _GetHTML(MyResponse, encoding, bufflen);
            }
            catch (Exception erro)
            {
                //if (erro.Message.Contains("连接") && IAfreshTime - IErrorTime > 0)
                //{
                    //IErrorTime++;
                    return _GetHTML(URL, Accept, encoding, bufflen);
                //}
            }
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="MyResponse"></param>
        /// <param name="encoding">字符编码</param>
        /// <param name="bufflen">数据包大小</param>
        /// <returns></returns>
        private string _GetHTML(HttpWebResponse MyResponse, Encoding encoding, int bufflen)
        {
            using (Stream MyStream = MyResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(MyStream);
                return sr.ReadToEnd();
              //long  ProgMaximum = MyResponse.ContentLength;
              //  string result = null;
              //  long totalDownloadedByte = 0;
              //  byte[] by = new byte[bufflen];
              //  int osize = MyStream.Read(by, 0, by.Length);
              //  long ProgValue = 0;
              //  while (osize > 0)
              //  {
              //      totalDownloadedByte = osize + totalDownloadedByte;
              //      result += encoding.GetString(by, 0, osize);
              //      ProgValue = totalDownloadedByte;
              //      osize = MyStream.Read(by, 0, by.Length);
              //  }
              //  MyStream.Close();
              //  return result;
            }
        }
    }
}
