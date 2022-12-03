using System;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests;

public class DriverTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateDriverTest_IdOnly_Positive()
    {
        var driver = new Driver();
        
        Assert.AreNotEqual(Guid.Empty, driver.Id);
    }

    [Test]
    public void CreateDriverTest_WithDriverData_Positive()
    {
        var name = "Sergio";
        var lastName = "Ramos";
        var birthday = DateTime.Now.AddYears(-20);
        var driver = new Driver(name, lastName, birthday);
        
        Assert.AreNotEqual(Guid.Empty, driver.Id);
        Assert.AreEqual(name, driver.Name);
        Assert.AreEqual(lastName, driver.LastName);
        Assert.AreEqual(birthday, driver.Birthday);
        Assert.IsNull(driver.SecondName);
    }

    [Test]
    [TestCase("Sergio", "García", "Ramos")]
    [TestCase("Sergio", null, "Ramos")]
    public void CreateDriverTest_WithDriverDataWithSecondName_Positive(string name, string secondName, string lastName)
    {
        var birthday = DateTime.Now.AddYears(-20);
        var driver = new Driver(name, secondName, lastName, birthday);
        
        Assert.AreNotEqual(Guid.Empty, driver.Id);
        Assert.AreEqual(name, driver.Name);
        Assert.AreEqual(lastName, driver.LastName);
        Assert.AreEqual(birthday, driver.Birthday);
        Assert.AreEqual(secondName, driver.SecondName);
    }

    [Test]
    [TestCase(null, "García", "Ramos")]
    [TestCase("Sergio", "García", null)]
    public void CreateDriverTest_Negative_ThrowsArgumentNullException(string name, string secondName, string lastName)
    {
        var birthday = DateTime.Now.AddYears(-20);
        var driver = () => new Driver(name, secondName, lastName, birthday);

        driver.Should().Throw<ArgumentNullException>();
    }

    [Test]
    [TestCase("S", "García", "Ramos", 20)]
    [TestCase("Sergio", "G", "Ramos", 20)]
    [TestCase("Sergio", "García", "", 20)]
    [TestCase("Sergio", "García", "Ramos", 10)]
    public void CreateDriverTest_Negative_ThrowsIncorrectRangeException(string name, string secondName, string lastName, int yearDifference)
    {
        var birthday = DateTime.Now.AddYears(-yearDifference);
        var driver = () => new Driver(name, secondName, lastName, birthday);

        driver.Should().Throw<IncorrectRangeException>();
    }
}