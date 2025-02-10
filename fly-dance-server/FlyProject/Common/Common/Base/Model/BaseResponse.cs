namespace Common.Base.Model
{
    public class BaseResponse<T>
    {
        public bool Isok { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }

        public T Data { get; set; }
    }
}
