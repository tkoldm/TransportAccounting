namespace Domain.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Check string length
    /// </summary>
    /// <remarks>Method returns true if input.Length more or equals minSize and input.Length less or equals maxSize</remarks>
    /// <param name="input">Input string that need be checked</param>
    /// <param name="minSize">Minimum string length for input param</param>
    /// <param name="maxSize">Maximum string length for input param</param>
    /// <returns>true - if input.Length in [minSize, maxSize]<br/>false - if input.Lenth not in [minSize, maxSize]</returns>
    public static bool CheckStringLength(this string input, int minSize, int maxSize)
    {
        return input.Length >= minSize && input.Length <= maxSize;
    }
}