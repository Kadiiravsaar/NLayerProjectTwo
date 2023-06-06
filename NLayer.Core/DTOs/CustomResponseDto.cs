using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        T Data { get; set; }
        List<string> Error { get; set; }
        int StatusCode { get; set; }

        public static CustomResponseDto<T> Success(int statusCode , T data)
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

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Error = errors

            };
        }
        public static CustomResponseDto<T> Fail(int statusCode,string error)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Error = new List<string> { error }

            };
        }

    }
}
