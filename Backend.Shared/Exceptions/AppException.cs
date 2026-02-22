using Microsoft.AspNetCore.Http;

namespace Backend.Shared.Exceptions;

public class AppException(int code, string message) : Exception(message)
{
    public int Code { get; } = code;
}

public sealed class UnauthorizedAppException(string message = "Unauthorized") 
    : AppException(StatusCodes.Status401Unauthorized, message);
public sealed class AlreadyExistsAppException(string message) 
    : AppException(StatusCodes.Status409Conflict, message);
public sealed class NotFoundAppException(string message)
    : AppException(StatusCodes.Status404NotFound, message);