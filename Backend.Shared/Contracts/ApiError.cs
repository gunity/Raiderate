namespace Backend.Shared.Contracts;

public sealed record ApiError(int Code, string Message, string TraceId);