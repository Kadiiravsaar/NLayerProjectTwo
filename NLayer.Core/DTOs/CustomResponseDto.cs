using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Error { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

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

        public static CustomResponseDto<T> Fail(int statusCode)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,

            };
        }


        public static CustomResponseDto<T> Fail( string error)
        {
            return new CustomResponseDto<T>
            {

                Message = error 

            };
        }

    }
}
