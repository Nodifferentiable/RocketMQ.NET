using ons;
using Kimi.RocketMQ.NET.Helper;
using System;
using System.Text;

namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// 消息队列-RocketMQ生产者
    /// </summary>
    public class QueueOnsProducer
    {
        private static Producer _producer;
        private static PushConsumer _consumer;
        private static string Ons_Topic = "";
        private static string Ons_AccessKey = "";
        private static string Ons_SecretKey = "";
        private static string Ons_GroupId = "";
        private static string Ons_NameSrv = "";
        private static int Ons_ConsumptionPattern = 1;
        private static string Ons_Client_Code = "Test_RocketMQ_Producer";
        private const string Ons_LogPath = "C://rocket_mq_logs";

        public static string getOnsTopic
        {
            get
            {
                return Ons_Topic;
            }
        }

        public static string getOnsClientCode
        {
            get
            {
                return Ons_Client_Code;
            }
        }

        private static ONSFactoryProperty getFactoryPropertyProducer()
        {
            ONSFactoryProperty factoryInfo = new ONSFactoryProperty();
            factoryInfo.setFactoryProperty(ONSFactoryProperty.AccessKey, Ons_AccessKey);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.SecretKey, Ons_SecretKey);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.ConsumerId, Ons_GroupId);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.ProducerId, Ons_GroupId);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.PublishTopics, Ons_Topic);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.NAMESRV_ADDR, Ons_NameSrv);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.LogPath, Ons_LogPath);
            return factoryInfo;
        }

        private static ONSFactoryProperty getFactoryPropertyConsumer()
        {
            ONSFactoryProperty factoryInfo = new ONSFactoryProperty();
            factoryInfo.setFactoryProperty(ONSFactoryProperty.AccessKey, Ons_AccessKey);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.SecretKey, Ons_SecretKey);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.ConsumerId, Ons_GroupId);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.PublishTopics, Ons_Topic);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.NAMESRV_ADDR, Ons_NameSrv);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.LogPath, Ons_LogPath);
            //消费模式（1:集群消费、2:广播消费）
            if (Ons_ConsumptionPattern == 1)
            {
                factoryInfo.setFactoryProperty(ONSFactoryProperty.MessageModel, ONSFactoryProperty.CLUSTERING);
            }
            else if (Ons_ConsumptionPattern == 2)
            {
                factoryInfo.setFactoryProperty(ONSFactoryProperty.MessageModel, ONSFactoryProperty.BROADCASTING);
            }
            return factoryInfo;
        }

        public static void CreateProducer(ONSPropertyConfigModel config)
        {
            if (config == null) { throw new ArgumentNullException("config is null"); }
            if (string.IsNullOrEmpty(config.AccessKey)) { throw new ArgumentNullException("AccessKey is null"); }
            if (string.IsNullOrEmpty(config.SecretKey)) { throw new ArgumentNullException("SecretKey is null"); }
            if (string.IsNullOrEmpty(config.GroupId)) { throw new ArgumentNullException("GroupId is null"); }
            if (string.IsNullOrEmpty(config.Topics)) { throw new ArgumentNullException("Topics is null"); }
            if (string.IsNullOrEmpty(config.NAMESRV_ADDR)) { throw new ArgumentNullException("NAMESRV_ADDR is null"); }
            if (string.IsNullOrEmpty(config.OnsClientCode)) { throw new ArgumentNullException("OnsClientCode is null"); }

            Ons_AccessKey = config.AccessKey;
            Ons_SecretKey = config.SecretKey;
            Ons_GroupId = config.GroupId;
            Ons_Topic = config.Topics;
            Ons_NameSrv = config.NAMESRV_ADDR;
            Ons_Client_Code = config.OnsClientCode;
            _producer = ONSFactory.getInstance().createProducer(getFactoryPropertyProducer());
        }

        public static void StartProducer()
        {
            if (_producer != null)
            {
                _producer.start();
            }
            else
            {
                throw new ArgumentNullException("_producer is null，请先执行[CreateProducer]创建生产者后启动");
            }
        }

        public static void ShutdownProducer()
        {
            if (_producer != null)
            {
                _producer.shutdown();
            }
        }

        public static string SendMessage(QueueOnsCommonModel model, string tag = "RegisterLog")
        {
            if (model == null) { throw new ArgumentNullException("model is null"); }

            model.SendTime = DateTime.Now;
            model.Source = Ons_Client_Code;
            var send_str = JsonUtility.ModelToJson(model);
            byte[] bytes = Encoding.UTF8.GetBytes(send_str);
            string str_new_msg = Encoding.Default.GetString(bytes);

            string msg_key = model.MessageId;
            string msg_id = string.Empty;
            Message msg = new Message(Ons_Topic, tag, str_new_msg);
            msg.setKey(msg_key);
            try
            {
                SendResultONS sendResult = _producer.send(msg);
                msg_id = sendResult.getMessageId();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg_id;
        }

        public static void CreatePushConsumer(ONSPropertyConfigModel config)
        {
            if (config == null) { throw new ArgumentNullException("config is null"); }
            if (string.IsNullOrEmpty(config.AccessKey)) { throw new ArgumentNullException("AccessKey is null"); }
            if (string.IsNullOrEmpty(config.SecretKey)) { throw new ArgumentNullException("SecretKey is null"); }
            if (string.IsNullOrEmpty(config.GroupId)) { throw new ArgumentNullException("GroupId is null"); }
            if (string.IsNullOrEmpty(config.Topics)) { throw new ArgumentNullException("Topics is null"); }
            if (string.IsNullOrEmpty(config.NAMESRV_ADDR)) { throw new ArgumentNullException("NAMESRV_ADDR is null"); }
            if (string.IsNullOrEmpty(config.OnsClientCode)) { throw new ArgumentNullException("OnsClientCode is null"); }

            // 集群消费。
            Ons_ConsumptionPattern = 1;
            // 广播消费。
            //Ons_ConsumptionPattern = 2;

            Ons_AccessKey = config.AccessKey;
            Ons_SecretKey = config.SecretKey;
            Ons_GroupId = config.GroupId;
            Ons_Topic = config.Topics;
            Ons_NameSrv = config.NAMESRV_ADDR;
            Ons_Client_Code = config.OnsClientCode;
            _consumer = ONSFactory.getInstance().createPushConsumer(getFactoryPropertyConsumer());
        }

        public static void SetPushConsumer(MessageListener listener, string subExpression = "*")
        {
            if(_consumer != null)
            {
                _consumer.subscribe(Ons_Topic, subExpression, listener);
            }
            else
            {
                throw new ArgumentNullException("_consumer is null，请先执行[CreatePushConsumer]创建消费者后启动");
            }
        }

        public static void StartPushConsumer()
        {
            if (_consumer != null)
            {
                _consumer.start();
            }
            else
            {
                throw new ArgumentNullException("_consumer is null，请先执行[CreatePushConsumer]创建消费者后启动");
            }
        }

        public static void ShutdownPushConsumer()
        {
            if (_consumer != null)
            {
                _consumer.shutdown();
            }
        }
    }
}
