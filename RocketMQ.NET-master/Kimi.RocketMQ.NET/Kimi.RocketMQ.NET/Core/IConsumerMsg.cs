namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// 消费接口
    /// </summary>
    public interface IConsumerMsg
    {
        void Consume(QueueOnsCommonModel model);
    }
}
