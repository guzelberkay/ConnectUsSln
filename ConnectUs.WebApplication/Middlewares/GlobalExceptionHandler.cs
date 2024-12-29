using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectUs.Core.Exceptions
{
    public enum ErrorType
    {
        INTERNAL_SERVER_ERROR = 1000,
        BAD_REQUEST_ERROR = 1001,
        // Diğer hata türleri burada yer alabilir...
    }

    public static class ErrorTypeExtensions
    {
        public static int GetCode(this ErrorType errorType)
        {
            return (int)errorType;
        }

        public static string GetMessage(this ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.INTERNAL_SERVER_ERROR:
                    return "An unexpected error occurred on the server. Please try again.";
                case ErrorType.BAD_REQUEST_ERROR:
                    return "The provided parameters are incorrect. Please correct them and try again.";
                // Diğer hata mesajları burada yer alabilir...
                default:
                    return "Unknown error.";
            }
        }
    }

    public class ResponseDTO<T>
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
    }

    // Global Hata Yönetimi için Filter
    public class GlobalExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Hata türünü belirle
            ErrorType errorType = ErrorType.INTERNAL_SERVER_ERROR;

            if (exception is GeneralException generalException)
            {
                errorType = generalException.ErrorType;
            }

            // Hata mesajını oluştur
            var response = new ResponseDTO<string>
            {
                Message = exception.Message,
                Code = errorType.GetCode(),
                Data = ""
            };

            // Loglama işlemi
            _logger.LogError($"Exception caught: {exception}");

            // Yanıtı döndür
            context.Result = new JsonResult(response)
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError // Bu kısmı errorType'a göre güncelleyebilirsiniz
            };

            context.ExceptionHandled = true; // Hata işlenmiş oldu
        }
    }

    // GeneralException sınıfı
    public class GeneralException : Exception
    {
        public ErrorType ErrorType { get; }

        public GeneralException(ErrorType errorType)
            : base(errorType.GetMessage())
        {
            ErrorType = errorType;
        }
    }
}
