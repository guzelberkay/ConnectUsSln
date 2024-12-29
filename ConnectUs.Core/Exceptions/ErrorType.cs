using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Core.Exceptions
{
    public enum ErrorType
    {
        INTERNAL_SERVER_ERROR = 1000,
        BAD_REQUEST_ERROR = 1001,
        INVALID_TOKEN = 1006,
        USER_NOT_FOUND = 1007,
        PASSWORD_MISMATCH = 1011,
        TOKEN_CREATION_FAILED = 1013,
        USER_IS_ACTIVE = 1016,
        EMAIL_OR_PASSWORD_WRONG = 1018,
        PASSWORD_WRONG = 1019,
        TOKEN_FORMAT_NOT_ACCEPTABLE = 1020,
        TOKEN_VERIFY_FAILED = 1021,
        AUTH_NOT_FOUND = 1022,
        PROJECT_NOT_FOUND = 1023,
        COMMENT_NOT_FOUND = 1024,
        CONTACT_NOT_FOUND = 1025,
        COMMENT_ALREADY_APPROVED_OR_REJECTED = 1026,
        OURSERVICES_NOT_FOUND = 1027,
        TOKEN_INVALID = 1031,
        EMAIL_SEND_FAILED = 1032,
        EXPIRED_RESET_CODE = 1033
    }

    public static class ErrorTypeExtensions
    {
        public static string GetMessage(this ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.INTERNAL_SERVER_ERROR:
                    return "An unexpected error occurred on the server. Please try again.";
                case ErrorType.BAD_REQUEST_ERROR:
                    return "The provided parameters are incorrect. Please correct them and try again.";
                case ErrorType.INVALID_TOKEN:
                    return "The provided token is invalid. Please try again.";
                case ErrorType.USER_NOT_FOUND:
                    return "User not found. Please try again.";
                case ErrorType.PASSWORD_MISMATCH:
                    return "Passwords do not match. Please correct them and try again.";
                case ErrorType.TOKEN_CREATION_FAILED:
                    return "Token creation failed. Please try again.";
                case ErrorType.USER_IS_ACTIVE:
                    return "User is active.";
                case ErrorType.EMAIL_OR_PASSWORD_WRONG:
                    return "Email or password is incorrect. Please try again.";
                case ErrorType.PASSWORD_WRONG:
                    return "Password is incorrect. Please try again.";
                case ErrorType.TOKEN_FORMAT_NOT_ACCEPTABLE:
                    return "Token format is not acceptable. Please check and try again.";
                case ErrorType.TOKEN_VERIFY_FAILED:
                    return "Token verification failed. Please try again later.";
                case ErrorType.AUTH_NOT_FOUND:
                    return "Authentication not found. Please try again.";
                case ErrorType.PROJECT_NOT_FOUND:
                    return "Project not found. Please try again.";
                case ErrorType.COMMENT_NOT_FOUND:
                    return "Comment not found. Please try again.";
                case ErrorType.CONTACT_NOT_FOUND:
                    return "Contact not found. Please try again.";
                case ErrorType.COMMENT_ALREADY_APPROVED_OR_REJECTED:
                    return "The comment has already been approved or rejected.";
                case ErrorType.OURSERVICES_NOT_FOUND:
                    return "Our Service not found. Please try again.";
                case ErrorType.TOKEN_INVALID:
                    return "The provided token is invalid. Please try again.";
                case ErrorType.EMAIL_SEND_FAILED:
                    return "Email sending failed. Please try again.";
                case ErrorType.EXPIRED_RESET_CODE:
                    return "Reset code has expired.";
                default:
                    return "Unknown error.";
            }
        }

        public static HttpStatusCode GetHttpStatusCode(this ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.INTERNAL_SERVER_ERROR:
                case ErrorType.TOKEN_CREATION_FAILED:
                case ErrorType.EMAIL_SEND_FAILED:
                    return HttpStatusCode.InternalServerError;
                case ErrorType.BAD_REQUEST_ERROR:
                case ErrorType.PASSWORD_MISMATCH:
                case ErrorType.EMAIL_OR_PASSWORD_WRONG:
                case ErrorType.PASSWORD_WRONG:
                case ErrorType.TOKEN_FORMAT_NOT_ACCEPTABLE:
                case ErrorType.COMMENT_ALREADY_APPROVED_OR_REJECTED:
                    return HttpStatusCode.BadRequest;
                case ErrorType.INVALID_TOKEN:
                case ErrorType.TOKEN_INVALID:
                    return HttpStatusCode.Unauthorized;
                case ErrorType.USER_NOT_FOUND:
                case ErrorType.AUTH_NOT_FOUND:
                case ErrorType.PROJECT_NOT_FOUND:
                case ErrorType.COMMENT_NOT_FOUND:
                case ErrorType.CONTACT_NOT_FOUND:
                case ErrorType.OURSERVICES_NOT_FOUND:
                    return HttpStatusCode.NotFound;
                case ErrorType.TOKEN_VERIFY_FAILED:
                    return HttpStatusCode.ServiceUnavailable;
                case ErrorType.USER_IS_ACTIVE:
                    return HttpStatusCode.BadRequest;
                case ErrorType.EXPIRED_RESET_CODE:
                    return HttpStatusCode.InternalServerError;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}

