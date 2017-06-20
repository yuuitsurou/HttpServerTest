using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerTest2
{
    /// <summary>
    /// 参考 : http://blog.clock-up.jp/entry/2016/12/01/csharp-http-server
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            FileServer();
        }

        static void FileServer()
        {
            String wwwroot = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\wwwroot");

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:1000/");
            listener.Start();

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest req = context.Request;
                HttpListenerResponse res = context.Response;

                String url = req.RawUrl;
                // Console.WriteLine(url);
                if (url == "/") { url = "/index.html"; }

                String path = wwwroot + url.Replace("/", "\\");

                try
                {
                    res.StatusCode = 200;
                    byte[] content = File.ReadAllBytes(path);
                    res.OutputStream.Write(content, 0, content.Length);
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500;
                    byte[] content = Encoding.UTF8.GetBytes(ex.Message);
                    res.OutputStream.Write(content, 0, content.Length);
                }
                res.Close();
            }
        }
    }
}

