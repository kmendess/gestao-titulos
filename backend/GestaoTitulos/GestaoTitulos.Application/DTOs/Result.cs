namespace GestaoTitulos.Application.DTOs
{
    public class Result
    {
        public bool IsSuccess { get; protected set; } = true;
        public List<string> Messages { get; protected set; } = [];

        public static Result Success() => new();

        public static Result Error(params string[] messages)
        {
            return new Result
            {
                IsSuccess = false,
                Messages = messages.ToList()
            };
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; private set; }

        public static Result<T> Success(T data)
        {
            return new() { Data = data };
        }

        public static new Result<T> Error(params string[] messages)
        {
            return new()
            {
                IsSuccess = false,
                Messages = messages.ToList()
            };
        }
    }
}
