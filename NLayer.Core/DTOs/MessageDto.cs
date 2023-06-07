using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class MessageDto<T>
    {
        public string Mesaj { get; set; }

        public static MessageDto<T> Message(string message)
        {

            return new MessageDto<T>()
            {
                Mesaj = message
            };
            
           
        }
    }
}
