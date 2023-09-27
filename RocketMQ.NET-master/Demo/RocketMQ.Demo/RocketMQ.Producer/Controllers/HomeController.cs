using RocketMQ.Core.Models;
using RocketMQ.Producer.Services;
using System.Web.Mvc;

namespace RocketMQ.Producer.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            //调用消息队列
            new SampleProducerService().SendTestMessageHandle(new RocketMQSampleModel()
            {
                user_name = "西瓜程序猿",
                user_account = "admin"
            });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}