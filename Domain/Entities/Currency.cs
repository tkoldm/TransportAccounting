using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.Entities;

public class Currency
{
    /// <summary>
    /// Identifier of currency
    /// </summary>
    /// <example>1</example>
    public int Id { get; }

    /// <summary>
    /// Date of current course value
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Name of currency
    /// </summary>
    /// <example>USD</example>
    public string CurrencyName { get; private set; }

    /// <summary>
    /// Currency course value
    /// </summary>
    /// <example>3.36</example>
    public decimal CourseValue { get; set; }

    public Currency(int id, DateTime date, string currencyName, decimal courseValue)
    {
        Id = id;
        Date = date;
        SetCurrencyName(currencyName);
        SetCourseValue(courseValue);
    }

    /// <summary>
    /// Updates currency name
    /// </summary>
    /// <param name="newName" example="MDL">New value for property CurrencyName</param>
    public void UpdateCurrencyName(string newName)
    {
        CurrencyName = newName;
    }

    /// <summary>
    /// Update currency course value
    /// </summary>
    /// <param name="newValue" examle ="4.05">New value for property CourseValue</param>
    public void UpdateCourseValue(decimal newValue)
    {
        CourseValue = newValue;
    }

    private void SetCurrencyName(string currencyName)
    {
        if (currencyName == null)
        {
            throw new ArgumentNullException(nameof(currencyName), "Name of currency is required");
        }

        if (!currencyName.CheckStringLength(2, 10))
        {
            throw new IncorrectRangeException("Length of currency name must be 2..10");
        }

        CurrencyName = currencyName;
    }

    private void SetCourseValue(decimal courseValue)
    {
        if (!courseValue.IsCorrect(-1, 0))
        {
            throw new ArgumentException("Course value must be more than 0");
        }

        CourseValue = courseValue;
    }
}