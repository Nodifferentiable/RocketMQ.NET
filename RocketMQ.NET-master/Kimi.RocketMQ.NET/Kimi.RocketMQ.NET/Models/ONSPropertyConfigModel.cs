namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// RocketMQ配置属性
    /// </summary>
    public class ONSPropertyConfigModel
    {
        /// <summary>
        /// 设置为云消息队列 RocketMQ 版控制台实例详情页的实例用户名。
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 设置为云消息队列 RocketMQ 版控制台实例详情页的实例密码。
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 设置为您在云消息队列 RocketMQ 版控制台创建的Group ID。
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 您在云消息队列 RocketMQ 版控制台创建的Topic。
        /// </summary>
        public string Topics { get; set; }

        /// <summary>
        /// 设置为您从云消息队列 RocketMQ 版控制台获取的接入点信息，类似“rmq-cn-XXXX.rmq.aliyuncs.com:8080”
        /// </summary>
        public string NAMESRV_ADDR { get; set; }

        /// <summary>
        /// 消费者/生产者目标来源
        /// </summary>
        public string OnsClientCode { get; set; }
    }
}
