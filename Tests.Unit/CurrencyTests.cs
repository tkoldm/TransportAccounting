using System;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests;

public class CurrencyTests
{
    [TestCase(1, "USD", 4.52)]
    public void CurrencyCreate_Positive(int id, string currencyName, decimal courseValue)
    {
        var date = DateTime.Now;
        var currency = new Currency(id, date, currencyName, courseValue);

        Assert.AreNotEqual(Guid.Empty, currency.Id);
        Assert.AreEqual(date, currency.Date);
        Assert.AreEqual(currencyName, currency.CurrencyName);
        Assert.AreEqual(courseValue, currency.CourseValue);
    }

    [TestCase(1, null, 4.52)]
    public void CurrencyCreate_Negative_ThrowsArgumentNullException(int id, string currencyName, decimal courseValue)
    {
        var date = DateTime.Now;
        var currency = () => new Currency(id, date, currencyName, courseValue);

        currency.Should().Throw<ArgumentNullException>();
    }

    [TestCase(1, "U", 4.52)]
    [TestCase(1, "USDUSDUSDUSD", 4.52)]
    public void CurrencyCreate_Negative_ThrowsIncorrectRangeException(int id, string currencyName, decimal courseValue)
    {
        var date = DateTime.Now;
        var currency = () => new Currency(id, date, currencyName, courseValue);

        currency.Should().Throw<IncorrectRangeException>();
    }

    [TestCase(1, "USD", -1)]
    [TestCase(1, "USD", 0)]
    [TestCase(1, "USD", -25)]
    public void CurrencyCreate_Negative_ThrowsArgumentException(int id, string currencyName, decimal courseValue)
    {
        var date = DateTime.Now;
        var currency = () => new Currency(id, date, currencyName, courseValue);

        currency.Should().Throw<ArgumentException>();
    }

    [TestCase("MDL")]
    public void UpdateCurrencyName_Positive(string newCurrencyName)
    {
        var id = 1;
        var date = DateTime.Now;
        var currencyName = "USD";
        var courseValue = 45;
        var currency = new Currency(id, date, currencyName, courseValue);
        
        currency.UpdateCurrencyName(newCurrencyName);
        
        Assert.AreEqual(newCurrencyName, currency.CurrencyName);
        
        currency.UpdateCourseValue(785);
    }
}