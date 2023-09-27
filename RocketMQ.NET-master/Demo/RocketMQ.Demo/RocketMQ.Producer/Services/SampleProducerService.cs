using Kimi.RocketMQ.NET.Helper;
using RocketMQ.Core.Models;
using RocketMQ.Core.Models.Consts;
using RocketMQ.Core.Models.Enums;

namespace RocketMQ.Producer.Services
{
    public class SampleProducerService : BaseProducerService
    {
        /// <summary>
        /// 发送RocketMQ测试消息
        /// </summary>
        /// <param name="model"></param>
        public void SendTestMessageHandle(RocketMQSampleModel model)
        {
            if (model == null) return;
            string msg_body = JsonUtility.ModelToJson<RocketMQSampleModel>(model);
            if (msg_body != null)
            {
                SendQueueOnsProducer(msg_body, QueueTagConsts.XG_Blog_Sample_Tag, QueueOnsEventType.RocketMQ_TEST);
            }
        }
    }
}