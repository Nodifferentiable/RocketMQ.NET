using System;

namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// 生产者/消费者公共模型实现
    /// </summary>
    public class QueueOnsCommonModel : IQueueOnsCommonModel
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 对应RocketMQ中Tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 消息来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public string Body { get; set; }
    }
}
