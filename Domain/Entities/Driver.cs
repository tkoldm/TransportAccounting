using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.Entities;

public class Driver
{
    /// <summary>
    /// Driver's identifier
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Driver's name
    /// </summary>
    /// <example>Alex</example>
    public string Name { get; private set; }

    /// <summary>
    /// Driver's second name
    /// </summary>
    public string? SecondName { get; private set; }

    /// <summary>
    /// Driver's last name
    /// </summary>
    /// <example>Telles</example>
    public string LastName { get; private set; }

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
    /// <param name="name" example="Alex">Driver's name</param>
    /// <param name="secondName" example="">Driver's second name</param>
    /// <param name="lastName" example="Telles">Driver's last name</param>
    /// <param name="birthday" example="1978.06.24">Driver's birthday</param>
    public Driver(string name, string secondName, string lastName, DateTime birthday)
    {
        ValidateName(name);
        ValidateName(lastName);
        ValidateName(secondName, false);
        ValidateBirthday(birthday);
        Id = Guid.NewGuid();
        Name = name;
        SecondName = secondName;
        LastName = lastName;
        Birthday = birthday;
    }

    /// <summary>
    /// Create driver instance with driver's data
    /// </summary>
    /// <param name="name" example="Alex">Driver's name</param>
    /// <param name="lastName" example="Telles">Driver's last name</param>
    /// <param name="birthday" example="1978.06.24">Driver's birthday</param>
    public Driver(string name, string lastName, DateTime birthday)
    {
        ValidateName(name);
        ValidateName(lastName);
        ValidateBirthday(birthday);
        Id = Guid.NewGuid();
        Name = name;
        LastName = lastName;
        Birthday = birthday;
        SecondName = null;
    }

    public void SetName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void SetLastName(string lastName)
    {
        ValidateName(lastName);
        LastName = lastName;
    }

    public void SetSecondName(string secondName)
    {
        ValidateName(secondName, false);
        SecondName = secondName;
    }

    public void SetBirthday(DateTime birthday)
    {
        ValidateBirthday(birthday);
        Birthday = birthday;
    }

    private void ValidateName(string name, bool isRequired = true)
    {
        if (isRequired && name == null)
        {
            throw new ArgumentNullException();
        } 
        else if (!isRequired && name == null)
        {
            return;
        }
        
        if (!name.CheckStringLength(2, 60))
        {
            throw new IncorrectRangeException("Parameter have incorrect length", nameof(name));
        }
    }

    private void ValidateBirthday(DateTime birthday)
    {
        if (DateTime.Now < birthday.AddYears(18))
        {
            throw new IncorrectRangeException("Driver is too young");
        }
    }
}