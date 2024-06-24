using System.Net;

namespace Estudo.TRIMANIA.Domain.Exceptions
{
    public class ServiceException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public ServiceException(string? message) : base(message)
        {
        }

        public ServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        } 
        
        public ServiceException(string? message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public ServiceException(string? message, Exception? innerException, HttpStatusCode httpStatusCode) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
