using System;

namespace Vadalo.Identity.Providers;

public sealed class MemberNodeModel(
    Guid id,
    byte status,
    string emailAddress,
    string lastName,
    string firstName,
    string? middleName
)
{
    public Guid Id = id;

    public byte Status = status;

    public string EmailAddress = emailAddress;

    public string LastName = lastName;

    public string FirstName = firstName;

    public string? MiddleName = middleName;
}