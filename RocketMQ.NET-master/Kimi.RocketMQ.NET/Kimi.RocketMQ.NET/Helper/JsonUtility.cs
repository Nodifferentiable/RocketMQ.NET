using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Kimi.RocketMQ.NET.Helper
{
    public class JsonUtility
    {
        /// <summary>
        /// Json转实体类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonString"></param>
        /// <returns></returns>
        public static T JsonToModel<T>(string JsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(JsonString)))
            {
                try
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(T));
                    T returnOrg = (T)deseralizer.ReadObject(ms);
                    return returnOrg;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ms.Close();
                    ms.Dispose();
                }
            }
        }

        /// <summary>
        /// Json反序列化批量转换实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonString"></param>
        /// <returns></returns>
        public static List<T> JsonToList<T>(string JsonString)
        {
            return JsonToModel<List<T>>(JsonString);
        }

        /// <summary>
        /// 序列化json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ModelToJson<T>(T t)
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                MemoryStream mobj = new MemoryStream();
                js.WriteObject(mobj, t);
                mobj.Position = 0;
                StreamReader sr = new StreamReader(mobj, Encoding.UTF8);
                string Json = sr.ReadToEnd();
                sr.Close();
                mobj.Close();
                return Json;
            }
            catch
            {
                return null;
            }
        }
    }
}
