namespace Ingresso.Domain.InfoErrors
{
    public class InfoErrors
    {
        public bool IsSucess { get; set; }
        public string? Message { get; set; }

        public static InfoErrors Fail(string message) => new InfoErrors { IsSucess = false, Message = message };
        public static InfoErrors<T> Fail<T>(T data, string message) => new InfoErrors<T>(data, false, message);

        public static InfoErrors Ok(string message) => new InfoErrors { IsSucess = true, Message = message };
        public static InfoErrors<T> Ok<T>(T data) => new InfoErrors<T>(data, true);
    }

    public class InfoErrors<T> : InfoErrors
    {
        public T Data { get; set; }

        public InfoErrors(T data, bool isSucess)
        {
            Data = data;
            IsSucess = isSucess;
        }

        public InfoErrors(T data, bool isSucess, string message)
        {
            Data = data;
            IsSucess = isSucess;
            Message = message;
        }
    }
}