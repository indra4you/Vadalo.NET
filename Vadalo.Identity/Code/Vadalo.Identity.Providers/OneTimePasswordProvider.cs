using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Vadalo.Identity.Providers;

public sealed class OneTimePasswordProvider(
    OneTimePasswordOptions oneTimePasswordOptions
) : IPasswordProvider
{
    private readonly OneTimePasswordOptions _oneTimePasswordOptions = oneTimePasswordOptions;

    private string GenerateOneTimePassword(
    )
    {
        var randomCharacters = Enumerable
            .Repeat(
                this._oneTimePasswordOptions.OneTimePasswordAllowedCharacters,
                Convert
                    .ToInt32(
                        this._oneTimePasswordOptions.OneTimePasswordLength
                    )
            )
            .Select(
                s => s[Random.Shared.Next(s.Length)]
            )
            .ToArray();

        return new(
            randomCharacters
        );
    }

    private byte[] GenerateHash(
        string oneTimePassword
    )
    {
        var salt = RandomNumberGenerator
            .GetBytes(
                this._oneTimePasswordOptions.HashSaltSize
            );
        var hash = KeyDerivation
            .Pbkdf2(
                oneTimePassword,
                salt,
                this._oneTimePasswordOptions.HashPrf,
                this._oneTimePasswordOptions.HashIterations,
                this._oneTimePasswordOptions.HashNumberOfBytesRequested
            );

        var generatedHash = new byte[salt.Length  + hash.Length];
        Buffer
            .BlockCopy(
                salt,
                0,
                generatedHash,
                0,
                salt.Length
            );
        Buffer
            .BlockCopy(
                hash,
                0,
                generatedHash,
                salt.Length,
                hash.Length
            );

        return generatedHash;
    }

    public (
        string oneTimePassword,
        string passwordHash
    ) GeneratePassword(
    )
    {
        var oneTimePassword = this
            .GenerateOneTimePassword();
        var generatedHash = this
            .GenerateHash(
                oneTimePassword
            );
        var passwordHash = Convert
            .ToBase64String(
                generatedHash
            );

        return (
            oneTimePassword,
            passwordHash
        );
    }
}