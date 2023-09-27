using System;

namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public class EventTypeAttribute : Attribute
    {
        public string EventType { get; set; }

        public EventTypeAttribute(string eventType)
        {
            EventType = eventType;
        }
    }
}
