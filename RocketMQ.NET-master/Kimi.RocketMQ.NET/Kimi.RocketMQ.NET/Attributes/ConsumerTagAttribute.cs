using System;

namespace Kimi.RocketMQ.NET
{
    /// <summary>
    /// Tag标签
    /// </summary>
    public class ConsumerTagAttribute : Attribute
    {
        public string Tag { get; set; }

        public ConsumerTagAttribute(string tag)
        {
            Tag = tag;
        }
    }
}
