using System.ServiceProcess;

namespace RocketMQ.Consumer
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new RocketMQConsumerService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
