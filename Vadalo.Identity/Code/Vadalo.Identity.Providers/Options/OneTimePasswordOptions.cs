using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Vadalo.Identity.Providers;

public sealed class OneTimePasswordOptions
{
    public byte? OneTimePasswordLength { get; set; }

    internal string OneTimePasswordAllowedCharacters = "0123456789";

    internal byte HashSaltSize = (128 / 8);

    internal int HashIterations = 10000;

    internal KeyDerivationPrf HashPrf = KeyDerivationPrf.HMACSHA512;

    internal byte HashNumberOfBytesRequested = (256 / 8);
}