using log4net;
using System.Text;
using System;
using ons;
using Kimi.RocketMQ.NET.Helper;
using Kimi.RocketMQ.NET;

namespace RocketMQ.Consumer.Startup
{
    /// <summary>
    /// 消费端启动
    /// </summary>
    public class ConsumerStartup : MessageListener
    {
        private readonly static ILog logger = LogManager.GetLogger(typeof(ConsumerStartup));
        private readonly static ConsumerManager manager = new ConsumerManager();
        private readonly string _consumerClientCode;
        private readonly string _ons_groupId;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="consumerClientCode">消费者客户端Code</param>
        /// <param name="ons_groupId">消费者消费的分组</param>
        public ConsumerStartup(string consumerClientCode, string ons_groupId)
        {
            _consumerClientCode = consumerClientCode;
            _ons_groupId = ons_groupId;
        }

        ~ConsumerStartup()
        {
        }

        /// <summary>
        /// 消费者任务
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ons.Action consume(Message value, ConsumeContext context)
        {
            Console.WriteLine("【消费者任务】：消费者消息进来了...");
            logger.Info($"【消费者任务】：消费者消息进来了...");

            string topic = value.getTopic();
            string business_id = value.getKey();
            string message_id = value.getMsgID();
            string msg_tag = value.getTag();
            byte[] bytes = Encoding.Default.GetBytes(value.getBody());
            string msg_body = Encoding.Default.GetString(bytes);
            if (string.IsNullOrEmpty(msg_body))
            {
                return ons.Action.CommitMessage;
            };

            string log_body = $"本次消费的消息：【消费序列：{value.getQueueOffset()}】【消息key：{business_id}】【消息ID：{message_id}】【Tag：{msg_tag}】";
            Console.WriteLine(log_body);
            logger.Info(log_body);
            logger.Info($"【消费内容】：{msg_body}");

            int status = 1;
            string error_msg = "";
            long sys_msg_id = 0;
            QueueOnsCommonModel consumerModel = null;

            try
            {
                //调度到具体的消费者
                consumerModel = JsonUtility.JsonToModel<QueueOnsCommonModel>(msg_body);
                if (consumerModel != null)
                {
                    logger.Info($"【消费者任务】：真正开始执行了（消息key：{consumerModel.MessageId}）");
                    if (!long.TryParse(consumerModel.MessageId, out sys_msg_id))
                    {
                        logger.Info("sys_msg_id 转换失败！");
                    }

                    manager.ExecuteConsumer(consumerModel.Tag, consumerModel.EventType, consumerModel);

                    logger.Info($"【消费者任务】：执行完成了（消息key：{consumerModel.MessageId}）");
                }
                else
                {
                    status = 2;
                    error_msg = "【调度到具体的消费者】解析消息body内容为空，无法进行消费";
                    logger.Error($"【调度到具体的消费者】解析消息body内容为空，无法进行消费");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"【消费者任务】：发生异常了：{ex.Message}", ex);
                status = 2;
                error_msg = ex.Message;
            }

            return ons.Action.CommitMessage;
        }
    }
}
