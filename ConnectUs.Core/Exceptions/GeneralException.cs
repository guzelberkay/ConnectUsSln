using System;
using System.Net;

namespace ConnectUs.Core.Exceptions
{
    public class GeneralException : Exception
    {
        public ErrorType ErrorType { get; }

        // Hata fırlatma ve mesajı almak için constructor
        public GeneralException(ErrorType errorType)
            : base(errorType.GetMessage())  // Hata mesajını enum'dan alıyoruz
        {
            ErrorType = errorType;  // ErrorType özelliğini set ediyoruz
        }

        // HTTP durum kodunu almak için ek metod
        public HttpStatusCode GetHttpStatusCode()
        {
            return ErrorType.GetHttpStatusCode();  // Hata türüne göre uygun HTTP durum kodunu alıyoruz
        }
    }
}
