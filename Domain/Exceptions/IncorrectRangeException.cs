namespace Domain.Exceptions;

public class IncorrectRangeException : Exception
{
    public string? ParamName { get; }

    public IncorrectRangeException(string message, string paramName) : base(message)
    {
        ParamName = paramName;
    }

    public IncorrectRangeException(string message) : base(message)
    {
    }
}