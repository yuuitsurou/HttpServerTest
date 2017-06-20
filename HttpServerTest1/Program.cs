using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerTest
{
    /// <summary>
    /// 参考 : http://blog.clock-up.jp/entry/2016/12/01/csharp-http-server
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HttpListener listner = new HttpListener();
                listner.Prefixes.Add("http://localhost:10000/");
                listner.Start();
                while (true)
                {
                    HttpListenerContext context = listner.GetContext();
                    HttpListenerResponse response = context.Response;
                    response.StatusCode = 200;
                    byte[] content = Encoding.UTF8.GetBytes("Hello!");
                    response.OutputStream.Write(content, 0, content.Length);
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error: {0} [{1}]", ex.Message, ex.StackTrace));
            }
        }
    }
}

