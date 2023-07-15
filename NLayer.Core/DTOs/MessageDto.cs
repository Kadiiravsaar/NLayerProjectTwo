namespace NLayer.Core.DTOs
{
    public class MessageDto<T>
    {
        public string Mesaj { get; set; }
        public int StatusCode { get; set; }


        public static MessageDto<T> Message(string message, int statusCode)
        {

            return new MessageDto<T>()
            {
                Mesaj = message,
                StatusCode = statusCode
            };

        }

        public static MessageDto<T> Message(string message)
        {
            return new MessageDto<T>
            {
                Mesaj = message
            };
        }
    }
}
