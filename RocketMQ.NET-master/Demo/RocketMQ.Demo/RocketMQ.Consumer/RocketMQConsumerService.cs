using Kimi.RocketMQ.NET;
using log4net;
using log4net.Config;
using RocketMQ.Consumer.Config;
using RocketMQ.Consumer.Startup;
using System.ServiceProcess;

namespace RocketMQ.Consumer
{
    public partial class RocketMQConsumerService : ServiceBase
    {
        public RocketMQConsumerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("LogConfig/log4net.config"));

            ILog logger = LogManager.GetLogger(typeof(Program));
            logger.Info($"【RocketMQ.Consumer】应用开始启动了！");

            //创建消费者
            string ons_access_key = ConfigSetting.ons_access_key;
            string ons_secret_key = ConfigSetting.ons_secret_key;
            string ons_topic = ConfigSetting.ons_topic;
            string ons_groupId = ConfigSetting.ons_groupId;
            string ons_name_srv = ConfigSetting.ons_name_srv;
            string ons_client_code = ConfigSetting.ons_client_code;
            QueueOnsProducer.CreatePushConsumer(new ONSPropertyConfigModel()
            {
                AccessKey = ons_access_key,
                SecretKey = ons_secret_key,
                Topics = ons_topic,
                GroupId = ons_groupId,
                NAMESRV_ADDR = ons_name_srv,
                OnsClientCode = ons_client_code,
            });
            //设置消费者
            QueueOnsProducer.SetPushConsumer(new ConsumerStartup(ons_client_code, ons_groupId), "*");
            //启动消费者
            QueueOnsProducer.StartPushConsumer();
        }

        protected override void OnStop()
        {
        }
    }
}
