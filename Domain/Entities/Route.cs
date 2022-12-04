using System.Text.RegularExpressions;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.Entities;

/// <summary>
/// Information about transport company's route
/// </summary>
public class Route
{
    /// <summary>
    /// Identifier of route
    /// </summary>
    /// <example>859dc91c-fbb7-4502-86df-e55b2f450f16</example>
    public Guid Id { get; }

    /// <summary>
    /// Route's code
    /// </summary>
    /// <example>095</example>
    public string Code { get; }

    /// <summary>
    /// Route's name (DirectionFrom - DirectionTo)
    /// </summary>
    /// <example>Madrid - Paris</example>
    public string Name { get; }

    /// <summary>
    /// Route's direction type
    /// </summary>
    /// <example cref="DirectionType">DirectionType.Urban</example>
    public DirectionType DirectionType { get; }

    /// <summary>
    /// Time that need to complete route (in hours)
    /// </summary>
    /// <example>6.32</example>
    public decimal RouteTime { get; }

    /// <summary>
    /// Time that need to complete route (in hours)
    /// </summary>
    /// <example>7.10</example>
    public decimal FullRouteTime { get; }

    /// <summary>
    /// Count of route's iterations
    /// </summary>
    /// <example>6</example>
    public int TripCount { get; }

    /// <summary>
    /// Mileage per one-day at route (in km)
    /// </summary>
    /// <example>135.6</example>
    public decimal MileageOnRoute { get; }

    /// <summary>
    /// Common mileage on route (from the moment you leave the garage until you arrive at the garage) (in km)
    /// </summary>
    /// <example>140.9</example>
    public decimal FullMileageOnRoute { get; }

    /// <summary>
    /// Planned revenue
    /// </summary>
    /// <example>6500</example>
    public decimal Revenue { get; }

    /// <summary>
    /// Planned revenue in foreign currency
    /// </summary>
    /// <example>1200</example>
    public decimal RevenueForeignCurrency { get; }

    /// <summary>
    /// Foreign currency type
    /// </summary>
    /// <example>1</example>
    public int IdCurrency { get; }

    /// <summary>
    /// Create instance of route
    /// </summary>
    /// <param name="code" example="095">Route's code</param>
    /// <param name="name" example="Madrid - Paris">Route's name (DirectionFrom - DirectionTo)</param>
    /// <param name="directionType" example="DirectionType.Urban">Route's direction type</param>
    /// <param name="routeTime" example="6.32">Time that need to complete route at one iteration</param>
    /// <param name="fullRouteTime" example="7.10">Time that need to complete route</param>
    /// <param name="tripCount" example="6">Count of route's iterations</param>
    /// <param name="mileageOnRoute" example="135.6">Mileage per one-day at route</param>
    /// <param name="fullMileageOnRoute" example="140.9">Common mileage on route (from the moment you leave the garage until you arrive at the garage)</param>
    /// <param name="revenue" example="6500">Planned revenue</param>
    /// <param name="revenueForeignCurrency" example="1200">Planned revenue in foreign currency</param>
    /// <param name="idCurrency" example="1">Foreign currency type</param>
    public Route(string code, string name, DirectionType directionType, decimal routeTime, decimal fullRouteTime,
        int tripCount, decimal mileageOnRoute, decimal fullMileageOnRoute, decimal revenue,
        decimal revenueForeignCurrency, int idCurrency)
    {
        ValidateCode(code);
        ValidateName(name);
        ValidateDecimal(routeTime, nameof(routeTime));
        ValidateDecimal(fullRouteTime, nameof(fullRouteTime));
        ValidateDecimal(tripCount, nameof(tripCount));
        ValidateDecimal(mileageOnRoute, nameof(mileageOnRoute));
        ValidateDecimal(fullMileageOnRoute, nameof(fullMileageOnRoute));
        ValidateDecimal(revenue, nameof(revenue));
        ValidateDecimal(revenueForeignCurrency, nameof(revenueForeignCurrency));
        ValidateDecimal(idCurrency, nameof(idCurrency));
        Id = Guid.NewGuid();
        Code = code;
        Name = name;
        DirectionType = directionType;
        RouteTime = routeTime;
        FullRouteTime = fullRouteTime;
        TripCount = tripCount;
        MileageOnRoute = mileageOnRoute;
        FullMileageOnRoute = fullMileageOnRoute;
        Revenue = revenue;
        RevenueForeignCurrency = revenueForeignCurrency;
        IdCurrency = idCurrency;
    }

    private void ValidateDecimal(decimal value, string paramName)
    {
        if (!value.IsCorrect(0, -1))
        {
            throw new ArgumentException($"Parameter {paramName} is required. Incorrect value {value}", paramName);
        }
    }

    private void ValidateCode(string code)
    {
        if (code == null)
        {
            throw new ArgumentNullException(nameof(code), "Route's code is required");
        }

        if (!code.CheckStringLength(2, 5))
        {
            throw new IncorrectRangeException("Route's code range need be from 2 to 5 characters");
        }
    }

    private void ValidateName(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name), "Route's name is required");
        }

        if (!Regex.IsMatch(name, @"[a-zA-Zа-яА-Я]{3,}-[a-zA-Zа-яА-Я]{3,}"))
        {
            throw new IncorrectRangeException("Route's need to be in format \"Direction from - Direction to\"");
        }
    }
}