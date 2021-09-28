namespace SwitEventHandler
{
    class Program
    {
        const int SWIT_EVENT_PORT = 3033;

        static void Main(string[] args)
        {
            string prefix = string.Format("http://{0}:{1}/", "+", SWIT_EVENT_PORT);
            SwitEventHandler handler = new SwitEventHandler(prefix);
            handler.StartServer();
            handler.HandleSwitEvent();
            handler.StopServer();
        }
    }
}
