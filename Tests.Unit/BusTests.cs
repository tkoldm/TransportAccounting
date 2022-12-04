using System;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests;

public class BusTests
{
    [TestCase("65456", "A3244CR", "Sprinter", 0.96, 1)]
    public void CreateBus_Positive(string code, string number, string model, decimal consumption, int fuelType)
    {
        var bus = new Bus(code, number, model, consumption, fuelType);
        
        Assert.AreNotEqual(Guid.Empty, bus.Id);
        Assert.AreEqual(code, bus.Code);
        Assert.AreEqual(number, bus.Number);
        Assert.AreEqual(model, bus.Model);
        Assert.AreEqual(consumption, bus.Consumption);
        Assert.AreEqual(fuelType, bus.FuelType);
    }

    [TestCase(null, "A3244CR", "Sprinter", 0.96, 1)]
    [TestCase("65456", null, "Sprinter", 0.96, 1)]
    [TestCase("65456", "A3244CR", null, 0.96, 1)]
    public void CreateBus_Negative_ThrowsArgumentNullException(string code, string number, string model,
        decimal consumption, int fuelType)
    {
        var addBus = () => new Bus(code, number, model, consumption, fuelType);

        addBus.Should().Throw<ArgumentNullException>();
    }

    [TestCase("65456", "A324", "Sprinter", 0.96, 1)]
    public void CreateBus_Negative_ThrowsIncorrectRangeException(string code, string number, string model,
        decimal consumption, int fuelType)
    {
        var addBus = () => new Bus(code, number, model, consumption, fuelType);

        addBus.Should().Throw<IncorrectRangeException>();
    }

    [TestCase("65456", "A3244CR", "Sprinter", 0, 1)]
    [TestCase("65456", "A3244CR", "Sprinter", -1, 1)]
    [TestCase("65456", "A3244CR", "Sprinter", 0.96, 0)]
    [TestCase("65456", "A3244CR", "Sprinter", 0.96, -1)]
    [TestCase("65456", "A3244CR", "Sprinter", 0.96, -15)]
    public void CreateBus_Negative_ThrowsArgumentExceptionException(string code, string number, string model,
        decimal consumption, int fuelType)
    {
        var addBus = () => new Bus(code, number, model, consumption, fuelType);

        addBus.Should().Throw<ArgumentException>();
    }
}