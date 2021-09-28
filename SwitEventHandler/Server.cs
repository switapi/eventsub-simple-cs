using System;
using System.Net;

namespace SwitEventHandler
{
    public class SwitEventHandler
    {
        private HttpListener listener = null;
        private string prefix { get; set; }

        public SwitEventHandler(string prefix)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            // Create a listener.
            listener = new HttpListener();

            // Add the prefixes.
            this.prefix = prefix;
            listener.Prefixes.Add(prefix);
        }

        public void StartServer()
        {
            Console.WriteLine("Start server");

            try
            {
                listener.Start();
                Console.WriteLine("Listening... {0}", prefix);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void HandleSwitEvent()
        {
            bool active = true;

            try
            {
                while (active)
                {
                    IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
                    Console.WriteLine("Waiting for request to be processed asyncronously.");
                    result.AsyncWaitHandle.WaitOne();
                    Console.WriteLine("Request processed asyncronously.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hanlder Exception: {0}", e.ToString());
            }
        }

        public static void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext ctx = listener.EndGetContext(result);
            HttpListenerRequest req = ctx.Request;
            Console.WriteLine("{0} {1} HTTP/1.1", req.HttpMethod, req.RawUrl);
            Console.WriteLine("User-Agent: {0}", req.UserAgent);
            Console.WriteLine("Accept-Encoding: {0}", req.Headers["Accept-Encoding"]);
            Console.WriteLine("Connection: {0}", req.KeepAlive ? "Keep-Alive" : "close");
            Console.WriteLine("Host: {0}", req.UserHostName);

            if (req.HttpMethod == "POST")
            {
                Console.WriteLine("Route Path:{0}", req.Url.AbsolutePath);

                string route_path = req.Url.AbsolutePath.Trim('/');
                if (route_path == Route.SWIT_EVENT_ROUTE)
                {
                    Route.ProcessSwitEvent(ctx.Request, ctx.Response);
                }
                else
                {
                    Console.WriteLine("Undefined route path: {0}", route_path);
                }
            }

            Console.WriteLine();
        }

        public void StopServer()
        {
            Console.WriteLine("Stop server");

            try
            {
                listener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
