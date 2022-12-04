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
    public void CreateDriver_IdOnly_Positive()
    {
        var driver = new Driver();

        Assert.AreNotEqual(Guid.Empty, driver.Id);
    }

    [Test]
    public void CreateDriver_WithDriverData_Positive()
    {
        var code = "5596";
        var name = "Sergio";
        var lastName = "Ramos";
        var birthday = DateTime.Now.AddYears(-20);
        var driver = new Driver(code, name, lastName, birthday);

        Assert.AreNotEqual(Guid.Empty, driver.Id);
        Assert.AreEqual(code, driver.Code);
        Assert.AreEqual(name, driver.Name);
        Assert.AreEqual(lastName, driver.LastName);
        Assert.AreEqual(birthday, driver.Birthday);
        Assert.IsNull(driver.SecondName);
    }

    [Test]
    [TestCase("47866", "Sergio", "García", "Ramos")]
    [TestCase("47866", "Sergio", null, "Ramos")]
    public void CreateDriver_WithDriverDataWithSecondName_Positive(string code, string name, string secondName,
        string lastName)
    {
        var birthday = DateTime.Now.AddYears(-20);
        var driver = new Driver(code, name, secondName, lastName, birthday);

        Assert.AreNotEqual(Guid.Empty, driver.Id);
        Assert.AreEqual(name, driver.Name);
        Assert.AreEqual(lastName, driver.LastName);
        Assert.AreEqual(birthday, driver.Birthday);
        Assert.AreEqual(secondName, driver.SecondName);
    }

    [Test]
    [TestCase("47866", null, "García", "Ramos")]
    [TestCase("47866", "Sergio", "García", null)]
    [TestCase(null, "Sergio", "García", "Ramos")]
    public void CreateDriver_Negative_ThrowsArgumentNullException(string code, string name, string secondName,
        string lastName)
    {
        var birthday = DateTime.Now.AddYears(-20);
        var driver = () => new Driver(code, name, secondName, lastName, birthday);

        driver.Should().Throw<ArgumentNullException>();
    }

    [Test]
    [TestCase("47866", "S", "García", "Ramos", 20)]
    [TestCase("47866", "Sergio", "G", "Ramos", 20)]
    [TestCase("47866", "Sergio", "García", "R", 20)]
    [TestCase("47866", "Sergio", "García", "", 20)]
    [TestCase("47866", "Sergio", "García", "Ramos", 10)]
    [TestCase("47866", "S", null, "Ramos", 20)]
    [TestCase("47866", "Sergio", null, "R", 20)]
    [TestCase("47866", "Sergio", null, "Ramos", 10)]
    public void CreateDriver_Negative_ThrowsIncorrectRangeException(string code, string name, string secondName,
        string lastName, int yearDifference)
    {
        var birthday = DateTime.Now.AddYears(-yearDifference);
        Func<Driver> driver;
        if (secondName != null)
        {
            driver = () => new Driver(code, name, secondName, lastName, birthday);
        }
        else
        {
            driver = () => new Driver(code, name, lastName, birthday);
        }

        driver.Should().Throw<IncorrectRangeException>();
    }

    [TestCase("47866", "Sergio", "Ramos")]
    public void CreateDriver_Negative_ThrowsArgumentException(string code, string name, string lastName)
    {
        var driver = () => new Driver(code, name, lastName, new DateTime());

        driver.Should().Throw<ArgumentException>();
    }
}