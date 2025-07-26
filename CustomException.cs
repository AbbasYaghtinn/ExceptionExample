using System.Runtime.Serialization;
using System;

namespace AbsYtn.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        public string ErrorCode { get; }

        public CustomException(string message, string errorCode = "GENERIC_ERROR")
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public CustomException(string message, Exception innerException, string errorCode = "GENERIC_ERROR")
                : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = info.GetString(nameof(ErrorCode));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ErrorCode), ErrorCode);
            base.GetObjectData(info, context);
        }

    }


    public class NotFoundException : CustomException
    {
        public NotFoundException(string resourceName, string id)
        : base($"{resourceName} with ID {id} was not found.", "NOT_FOUND")
        {
        }
    }

    public class ValidationException : CustomException
    {
        public ValidationException(string message)
            : base(message, "VALIDATION_ERROR")
        {
        }
    }

    public class UnauthorizedAccessException : CustomException
    {
        public UnauthorizedAccessException(string message = "Unauthorized access.")
            : base(message, "UNAUTHORIZED")
        {
        }
    }

    public class ConflictException : CustomException
    {
        public ConflictException(string message)
            : base(message, "CONFLICT")
        {
        }
    }

    public class InternalServerErrorException : CustomException
    {
        public InternalServerErrorException(string message = "An internal server error occurred.")
            : base(message, "INTERNAL_SERVER_ERROR")
        {
        }
    }
}

