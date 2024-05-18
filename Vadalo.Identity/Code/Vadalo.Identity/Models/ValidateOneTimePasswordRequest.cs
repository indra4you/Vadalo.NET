namespace Vadalo.Identity;

public sealed class ValidateOneTimePasswordRequest
{
    public string? EmailAddress { get; set; }

    public string? OneTimePassword { get; set; }
}