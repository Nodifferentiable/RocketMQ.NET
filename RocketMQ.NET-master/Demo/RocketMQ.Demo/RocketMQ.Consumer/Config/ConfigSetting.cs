using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace RocketMQ.Consumer.Config
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class ConfigSetting
    {
        private static T TryGetValueFromConfig<T>(Func<string, T> parseFunc, Func<T> defaultTValueFunc, [CallerMemberName] string key = "", string supressKey = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(supressKey))
                {
                    key = supressKey;
                }

                var node = ConfigurationManager.AppSettings[key];
                return !string.IsNullOrEmpty(node) ? parseFunc(node) : defaultTValueFunc();
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        #region 消息队列：RocketMQ
        /// <summary>
        /// 设置为云消息队列 RocketMQ 版控制台实例详情页的实例用户名。
        /// </summary>
        public static string ons_access_key
        {
            get
            {
                return TryGetValueFromConfig(_ => _, () => string.Empty);
            }
        }

        /// <summary>
        /// 设置为云消息队列 RocketMQ 版控制台实例详情页的实例密码。
        /// </summary>
        public static string ons_secret_key
        {
            get
            {
                return TryGetValueFromConfig(_ => _, () => string.Empty);
            }
        }

        /// <summary>
        ///  您在云消息队列 RocketMQ 版控制台创建的Topic。
        /// </summary>
        public static string ons_topic
        {
            get
            {
                return TryGetValueFromConfig(_ => _, () => string.Empty);
            }
        }

        /// <summary>
        /// 设置为您在云消息队列 RocketMQ 版控制台创建的Group ID。
        /// </summary>
        public static string ons_groupId
        {
            get
            {
                return TryGetValueFromConfig(_ => _, () => string.Empty);
            }
        }

        /// <summary>
        /// 设置为您从云消息队列 RocketMQ 版控制台获取的接入点信息，类似“rmq-cn-XXXX.rmq.aliyuncs.com:8080”。
        /// </summary>
        public static string ons_name_srv
        {
            get
            {
                return TryGetValueFromConfig(_ => _, () => string.Empty);
            }
        }

        /// <summary>
        /// 消息来源（生产者/消费端客户端编码）
        /// </summary>
        public static string ons_client_code
        {
            get
            {
                return TryGetValueFromConfig(_ => _, () => string.Empty);
            }
        }
        #endregion
    }
}
