namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = "Default mesaj";

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Data = data
            };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,

            };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string errors)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = new List<string> { errors }

            };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = errors
            };
        }

    }
}
