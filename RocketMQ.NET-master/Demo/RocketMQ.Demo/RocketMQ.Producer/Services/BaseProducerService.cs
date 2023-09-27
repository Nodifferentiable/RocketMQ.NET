using Kimi.RocketMQ.NET;
using log4net;
using System;
using System.Threading.Tasks;

namespace RocketMQ.Producer.Services
{
    /// <summary>
    /// 生产者服务
    /// </summary>
    public class BaseProducerService
    {
        private readonly ILog logger = log4net.LogManager.GetLogger(typeof(BaseProducerService));

        public void SendQueueOnsProducer(string body, string msg_tag, string mgs_eventType)
        {
            if (string.IsNullOrEmpty(body)) { throw new ArgumentNullException("body is null"); }
            if (string.IsNullOrEmpty(msg_tag)) { throw new ArgumentNullException("msg_tag is null"); }
            if (string.IsNullOrEmpty(mgs_eventType)) { throw new ArgumentNullException("mgs_eventType is null"); }

            string ons_topic = QueueOnsProducer.getOnsTopic;
            string ons_client_code = QueueOnsProducer.getOnsClientCode;

            //TODO：这里需要生成唯一ID
            string businessId = "MQ_1001";

            logger.Info($"【发送RocketMQ消息队列消息】准备开始执行了：（消息key：{businessId}）（tag：{msg_tag}）（event_type：{mgs_eventType}）");
            logger.Info($"【发送RocketMQ消息队列消息】消息内容：{body}");

            // TODO：在这里可以持久化生产者消息
            logger.Info($"【发送RocketMQ消息队列消息】消息持久化成功！(消息主键id：{businessId})");

            Task.Run(() =>
            {
                try
                {
                    QueueOnsProducer.SendMessage(new QueueOnsCommonModel()
                    {
                        MessageId = businessId,
                        Tag = msg_tag,
                        EventType = mgs_eventType,
                        Body = body
                    }, msg_tag);
                    logger.Info($"【发送RocketMQ消息队列消息】消息发送成功！");
                }
                catch (Exception ex)
                {
                    logger.Error($"【发送RocketMQ消息队列消息】发生异常：{ex.Message}", ex);
                }
            });
        }
    }
}