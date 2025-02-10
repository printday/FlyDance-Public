namespace Common.Filters.Models
{
    /// <summary>
    /// 通用异常
    /// </summary>
    public class FlyException : ApplicationException
    {
        /// <summary>
        /// 错误码
        /// </summary>

        private readonly string errorCode;

        /// <summary>
        /// 
        /// </summary>
        public FlyException() : base("服务器繁忙，请稍后再试!")
        {
            this.errorCode = "Fly001";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public FlyException(string message = "服务器繁忙，请稍后再试!", string errorCode = "Fly001") : base(message)
        {
            this.errorCode = errorCode;
        }

        public string GetErrorCode()
        {
            return this.errorCode;
        }
    }
}
