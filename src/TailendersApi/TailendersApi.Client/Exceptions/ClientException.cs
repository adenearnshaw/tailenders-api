using System;

namespace TailendersApi.Client.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException(int responseCode, string message) : base(message)
        {
            ResponseCode = responseCode;
        }
        
        public int ResponseCode { get; }
    }
}
