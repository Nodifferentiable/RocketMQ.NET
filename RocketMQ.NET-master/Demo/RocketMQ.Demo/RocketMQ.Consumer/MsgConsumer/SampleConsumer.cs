using Kimi.RocketMQ.NET;
using log4net;
using RocketMQ.Core.Models.Consts;
using RocketMQ.Core.Models.Enums;
using System;

namespace RocketMQ.Consumer.MsgConsumer
{
    /// <summary>
    /// 消费者Sample
    /// </summary>
    [ConsumerTag(QueueTagConsts.XG_Blog_Sample_Tag)]
    [EventType(QueueOnsEventType.RocketMQ_TEST)]
    public class SampleConsumer :  IConsumerMsg
    {
        private readonly static ILog logger = LogManager.GetLogger(typeof(SampleConsumer));

        public void Consume(QueueOnsCommonModel model)
        {
            logger.Info($"【西瓜程序猿-消费者Sample】：测试消费者进来了");
            if (model != null)
            {
                Console.WriteLine("tag:" + model.Tag);
                Console.WriteLine("body" + model.Body);
            }
            Console.WriteLine("【西瓜程序猿-消费者Sample】消费成功了！");
        }
    }
}
