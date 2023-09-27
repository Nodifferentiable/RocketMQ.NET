using System;
using System.Collections.Generic;
using System.Linq;

namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// 消费客户端管理
    /// </summary>
    public class ConsumerManager
    {
        private Dictionary<string, Dictionary<string, Type>> consumers;

        public ConsumerManager()
        {
            consumers = new Dictionary<string, Dictionary<string, Type>>();
            var consumerTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetCustomAttributes(typeof(ConsumerTagAttribute), false).Any());

            foreach (var consumerType in consumerTypes)
            {
                var tagAttr = (ConsumerTagAttribute)consumerType.GetCustomAttributes(typeof(ConsumerTagAttribute), false).FirstOrDefault();
                var eventTypeAttr = (EventTypeAttribute)consumerType.GetCustomAttributes(typeof(EventTypeAttribute), false).FirstOrDefault();

                if (tagAttr != null && eventTypeAttr != null)
                {
                    var tag = tagAttr.Tag;
                    var eventType = eventTypeAttr.EventType;

                    if (!consumers.ContainsKey(tag))
                    {
                        consumers[tag] = new Dictionary<string, Type>();
                    }

                    consumers[tag][eventType] = consumerType;
                }
            }
        }

        public void ExecuteConsumer(string tag, string eventType, QueueOnsCommonModel model)
        {
            if (consumers.TryGetValue(tag, out var eventTypes))
            {
                if (eventTypes.TryGetValue(eventType, out var consumerType))
                {
                    var consumer = (IConsumerMsg)Activator.CreateInstance(consumerType);
                    consumer.Consume(model);
                    return;
                }
            }
        }
    }
}
