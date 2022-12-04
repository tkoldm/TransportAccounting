using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests;

public class RouteTests
{
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    public void CreateRoute_Positive(string code, string name, DirectionType directionType, decimal routeTime,
        decimal fullRouteTime,
        int tripCount, decimal mileageOnRoute, decimal fullMileageOnRoute, decimal revenue,
        decimal revenueForeignCurrency, int idCurrency)
    {
        var route = new Route(code, name, directionType, routeTime, fullRouteTime, tripCount, mileageOnRoute,
            fullRouteTime, revenue, revenueForeignCurrency, idCurrency);

        Assert.AreNotEqual(Guid.Empty, route.Id);
        Assert.AreEqual(code, route.Code);
        Assert.AreEqual(name, route.Name);
        Assert.AreEqual(directionType, route.DirectionType);
        Assert.AreEqual(routeTime, route.RouteTime);
        Assert.AreEqual(fullRouteTime, route.FullRouteTime);
        Assert.AreEqual(tripCount, route.TripCount);
        Assert.AreEqual(mileageOnRoute, route.MileageOnRoute);
        Assert.AreEqual(fullRouteTime, route.FullMileageOnRoute);
        Assert.AreEqual(revenue, route.Revenue);
        Assert.AreEqual(revenueForeignCurrency, route.RevenueForeignCurrency);
        Assert.AreEqual(idCurrency, route.IdCurrency);
    }

    [TestCase(null, "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", null, DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    public void CreateRoute_Negative_WithNull_ThrowsArgumentNullException(string code, string name,
        DirectionType directionType, decimal routeTime,
        decimal fullRouteTime, int tripCount, decimal mileageOnRoute, decimal fullMileageOnRoute, decimal revenue,
        decimal revenueForeignCurrency, int idCurrency)
    {
        var addRoute = () => new Route(code, name, directionType, routeTime, fullRouteTime, tripCount, mileageOnRoute,
            fullRouteTime, revenue, revenueForeignCurrency, idCurrency);

        addRoute.Should().Throw<ArgumentNullException>();
    }

    [TestCase("4", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("456545", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Ma-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Pa", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Ma-Pa", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Ma4565-Pa4568", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 1)]
    public void CreateRoute_Negative_WithIncorrectRange_ThrowsIncorrectRangeException(string code, string name,
        DirectionType directionType, decimal routeTime,
        decimal fullRouteTime, int tripCount, decimal mileageOnRoute, decimal fullMileageOnRoute, decimal revenue,
        decimal revenueForeignCurrency, int idCurrency)
    {
        var addRoute = () => new Route(code, name, directionType, routeTime, fullRouteTime, tripCount, mileageOnRoute,
            fullRouteTime, revenue, revenueForeignCurrency, idCurrency);

        addRoute.Should().Throw<IncorrectRangeException>();
    }

    [TestCase("4565", "Madrid-Paris", DirectionType.International, 0, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 0, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 0, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 0, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 0, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 0, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 0, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, 0)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, -1, 12, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, -1, 3, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, -1, 123, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, -1, 130, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, -1, 6500.25, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, -1, 1200, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, -1, 1)]
    [TestCase("4565", "Madrid-Paris", DirectionType.International, 11, 12, 3, 123, 130, 6500.25, 1200, -1)]
    public void CreateRoute_Negative_WithIncorrectDecimals_ThrowsArgumentException(string code, string name,
        DirectionType directionType, decimal routeTime,
        decimal fullRouteTime, int tripCount, decimal mileageOnRoute, decimal fullMileageOnRoute, decimal revenue,
        decimal revenueForeignCurrency, int idCurrency)
    {
        var addRoute = () => new Route(code, name, directionType, routeTime, fullRouteTime, tripCount, mileageOnRoute,
            fullMileageOnRoute, revenue, revenueForeignCurrency, idCurrency);

        addRoute.Should().Throw<ArgumentException>();
    }
}