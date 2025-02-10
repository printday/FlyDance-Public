using Newtonsoft.Json;
using StackExchange.Redis;

namespace Common.Helpers
{
    /// <summary>
    /// redis 缓存操作类
    /// </summary>
    public class RedisHelper : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public ConnectionMultiplexer RedisConnection;

        /// <summary>
        /// 
        /// </summary>
        public IDatabase Cache;

        public RedisValue token = Environment.MachineName;

        public TimeSpan defaultExpTS = new TimeSpan(0, 20, 0);

        #region 构造函数
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public RedisHelper(string configuration)
        {
            this.RedisConnection = ConnectionMultiplexer.Connect(configuration);
            this.Cache = this.RedisConnection.GetDatabase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="password"></param>
        public RedisHelper(string host, int port, string password = null)
        {
            this.RedisConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                var configs = new ConfigurationOptions
                {
                    AbortOnConnectFail = false, // 允许重试连接
                    AllowAdmin = true,          // 如果需要执行管理命令
                    KeepAlive = 180,            // 保持连接活动的时间间隔
                    Password = password,
                    EndPoints = { { host, port } },
                    ConnectTimeout = 5000,      // 连接超时时间（毫秒）
                    SyncTimeout = 5000,         // 同步操作超时时间（毫秒）
                    ReconnectRetryPolicy = new LinearRetry(1000) // 每次重试等待1秒
                };
                return ConnectionMultiplexer.Connect(configs);
            }).Value;

            // 初始化 Cache 属性
            Cache = this.RedisConnection.GetDatabase();
        }
        #endregion

        #region Get
        /// <summary>
        /// Get Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            RedisValue values = this.Cache.StringGet(key);
            return ConvertObj<T>(values);
        }

        #endregion

        #region Remove
        /// <summary>
        /// remove key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return this.Cache.KeyDelete(key);
        }

        /// <summary>
        /// remove keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long RemoveAll(IEnumerable<string> keys)
        {
            var redisKeys = keys.Select(p => new RedisKey(p)).ToArray();

            return this.Cache.KeyDelete(redisKeys);
        }
        #endregion

        #region Set
        /// <summary>
        /// set value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value)
        {
            return this.Set(key, value, defaultExpTS);
        }

        /// <summary>
        /// set value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            try
            { 
                var json = ConvertJson(value);
                bool redisOk = RedisConnection.IsConnected;
                return this.Cache.StringSet(key, json, expiresIn); 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }
        #endregion

        #region Lock
        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool LockTake(string key, TimeSpan timeOut)
        {
            return this.Cache.LockTake(key, token, timeOut);
        }

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool LockTake(string key, string token, TimeSpan timeOut)
        {
            return this.Cache.LockTake(key, token, timeOut);
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool LockRelease(string key)
        {
            return this.Cache.LockRelease(key, token);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// to json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertJson<T>(T value)
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(string)) { return value.ToString(); }
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T ConvertObj<T>(RedisValue value)
        {
            if (value.IsNull) { return default(T); }
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                if (typeof(T) == typeof(bool)) { value = Convert.ToBoolean(value); }
                T ret = (T)Convert.ChangeType(value, typeof(T));
                return ret;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
        #endregion

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return this.Cache.KeyExists(key);
        }

        /// <summary>
        /// 设置有效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expiresIn)
        {
            return this.Cache.KeyExpire(key, expiresIn);
        }

        public void Dispose() { if (this.RedisConnection != null) this.RedisConnection.Dispose(); }
    }
}
