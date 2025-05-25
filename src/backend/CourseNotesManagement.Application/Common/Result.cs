namespace CourseNotesManagement.Application.Common;

public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? Error { get; set; }

    public static Result Ok(string? message = null) => new Result { Success = true, Message = message };
    public static Result Fail(string error) => new Result { Success = false, Error = error };
}

public class Result<T> : Result
{
    public T? Value { get; set; }

    public static Result<T> Ok(T value, string? message = null) =>
        new Result<T> { Success = true, Value = value, Message = message };

    public new static Result<T> Fail(string error) =>
        new Result<T> { Success = false, Error = error };
}