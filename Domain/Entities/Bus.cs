using System.Text.RegularExpressions;
using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.Entities;

public class Bus
{
    /// <summary>
    /// Identifier of bus
    /// </summary>
    /// <example>859dc91c-fbb7-4502-86df-e55b2f450f16</example>
    public Guid Id { get; }

    /// <summary>
    /// Bus code
    /// </summary>
    /// <example>095</example>
    public string Code { get; private set; }

    /// <summary>
    /// Government number of bus
    /// </summary>
    /// <example>AE75541V</example>
    public string Number { get; set; }

    /// <summary>
    /// Bus brand
    /// </summary>
    /// <example>Mersedes Sprinter</example>
    public string Model { get; set; }

    /// <summary>
    /// Fuel consumption
    /// </summary>
    /// <example>0.97</example>
    public decimal Consumption { get; }
    
    /// <summary>
    /// Fuel type
    /// </summary>
    /// <example>1</example>
    public int FuelType { get; }
    
    /// <summary>
    /// Create new instance of bus
    /// </summary>
    /// <param name="code" example="095">Bus code</param>
    /// <param name="number" example="AE75541V">Government number of bus</param>
    /// <param name="model" example="Mersedes Sprinter">Bus brand</param>
    /// <param name="consumption" example="0.97">Fuel consumption</param>
    /// <param name="fuelType" example="1">Fuel type</param>
    public Bus(string code, string number, string model, decimal consumption, int fuelType)
    {
        SetCode(code);
        SetNumber(number);
        SetModel(model);
        ValidateDecimal(consumption, nameof(consumption));
        ValidateDecimal(fuelType, nameof(fuelType));
        
        Id = Guid.NewGuid();
        Consumption = consumption;
        FuelType = fuelType;
    }

    private void SetCode(string code)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code), "Bus code is required");
    }

    private void ValidateDecimal(decimal value, string paramName)
    {
        if (!value.IsCorrect(0, -1))
        {
            throw new ArgumentException($"Parameter {paramName} is required. Incorrect value {value}", paramName);
        }
    }

    private void SetModel(string model)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model), "Bus brand model is required");
    }
    
    private void SetNumber(string number)
    {
        if (number == null)
        {
            throw new ArgumentNullException(nameof(number), "Bus code is required");
        }

        if (!Regex.IsMatch(number, @"[a-zA-Zа-яА-Я\d]{5,}"))
        {
            throw new IncorrectRangeException("Bus number must be minimum 5 characters with chars and digits");
        }

        Number = number;
    }
}