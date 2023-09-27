using System;

namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// 生产者/消费者公共模型接口
    /// </summary>
    public interface IQueueOnsCommonModel
    {
        /// <summary>
        /// 消息id
        /// </summary>
        string MessageId { get; set; }

        /// <summary>
        /// 对应RocketMQ中Tag
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        DateTime SendTime { get; set; }

        /// <summary>
        /// 消息来源
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        string EventType { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        string Body { get; set; }
    }
}
