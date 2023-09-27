using Kimi.RocketMQ.NET;
using log4net.Config;
using RocketMQ.Producer.Config;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RocketMQ.Producer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //创建生产者[西瓜程序猿]
            string ons_access_key = ConfigGeter.ons_access_key;
            string ons_secret_key = ConfigGeter.ons_secret_key;
            string ons_topic = ConfigGeter.ons_topic;
            string ons_groupId = ConfigGeter.ons_groupId;
            string ons_name_srv = ConfigGeter.ons_name_srv;
            string ons_client_code = ConfigGeter.ons_client_code;
            QueueOnsProducer.CreateProducer(new ONSPropertyConfigModel()
            {
                AccessKey = ons_access_key,
                SecretKey = ons_secret_key,
                Topics = ons_topic,
                GroupId = ons_groupId,
                NAMESRV_ADDR = ons_name_srv,
                OnsClientCode = ons_client_code,
            });
            //启动生产者
            QueueOnsProducer.StartProducer();
        }
    }
}
