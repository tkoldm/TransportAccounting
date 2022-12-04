using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.Entities;

/// <summary>
/// Information about person in transport company at position driver
/// </summary>
public class Driver
{
    /// <summary>
    /// Driver's identifier
    /// </summary>
    /// <example>859dc91c-fbb7-4502-86df-e55b2f450f16</example>
    public Guid Id { get; }

    /// <summary>
    /// Driver's code (in company)
    /// </summary>
    /// <example>45875</example>
    public string Code { get; private set; }

    /// <summary>
    /// Driver's name
    /// </summary>
    /// <example>Alex</example>
    public string Name { get; }

    /// <summary>
    /// Driver's second name
    /// </summary>
    public string? SecondName { get; }

    /// <summary>
    /// Driver's last name
    /// </summary>
    /// <example>Telles</example>
    public string LastName { get; }

    /// <summary>
    /// Driver's birthday
    /// </summary>
    /// <example>1978.06.24</example>
    public DateTime Birthday { get; private set; }

    /// <summary>
    /// Default driver creation
    /// </summary>
    public Driver()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Create driver instance with driver's data
    /// </summary>
    /// <param name="code" example="77854">Driver's code in company</param>
    /// <param name="name" example="Alex">Driver's name</param>
    /// <param name="secondName" example="">Driver's second name</param>
    /// <param name="lastName" example="Telles">Driver's last name</param>
    /// <param name="birthday" example="1978.06.24">Driver's birthday</param>
    public Driver(string code, string name, string secondName, string lastName, DateTime birthday)
    {
        SetCode(code);
        ValidateName(name);
        ValidateName(lastName);
        ValidateName(secondName, false);
        SetBirthday(birthday);
        Id = Guid.NewGuid();
        Name = name;
        SecondName = secondName;
        LastName = lastName;
    }

    /// <summary>
    /// Create driver instance with driver's data
    /// </summary>
    /// <param name="code" example="77854">Driver's code in company</param>
    /// <param name="name" example="Alex">Driver's name</param>
    /// <param name="lastName" example="Telles">Driver's last name</param>
    /// <param name="birthday" example="1978.06.24">Driver's birthday</param>
    public Driver(string code, string name, string lastName, DateTime birthday)
    {
        SetCode(code);
        ValidateName(name);
        ValidateName(lastName);
        SetBirthday(birthday);
        Id = Guid.NewGuid();
        Name = name;
        LastName = lastName;
        SecondName = null;
    }

    private void ValidateName(string name, bool isRequired = true)
    {
        if (isRequired && name == null)
        {
            throw new ArgumentNullException();
        }

        if (!isRequired && name == null)
        {
            return;
        }

        if (!name.CheckStringLength(2, 60))
        {
            throw new IncorrectRangeException("Parameter have incorrect length");
        }
    }

    private void SetCode(string code)
    {
        if (code == null)
        {
            throw new ArgumentNullException(nameof(code), "Driver's code is required");
        }

        Code = code;
    }

    private void SetBirthday(DateTime birthday)
    {
        if (birthday == default)
        {
            throw new ArgumentException("Driver's birthday is required");
        }
        if (DateTime.Now < birthday.AddYears(18))
        {
            throw new IncorrectRangeException("Driver is too young");
        }

        Birthday = birthday;
    }
}