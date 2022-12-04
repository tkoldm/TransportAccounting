namespace Domain.Extensions;

public static class DigitsExtensions
{
    public static bool IsCorrect(this decimal value, params decimal[] incorrect) =>
        !incorrect.Contains(value) && value >= 0;
}