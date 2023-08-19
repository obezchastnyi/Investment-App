using System;
using System.Security.Cryptography;
using InvestmentApp.Interfaces;
using Microsoft.Extensions.Logging;
using static InvestmentApp.Constants;

namespace InvestmentApp.Services;

public class PasswordManager : IPasswordManager
{
    private const int SaltSize = 16; // 128 bits
    private const int KeySize = 32; // 256 bits
    private const int Iterations = 50000;
    private const char SegmentDelimiter = ':';

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    private readonly ILogger<PasswordManager> _logger;

    public PasswordManager(ILogger<PasswordManager> logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public string Hash(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            this._logger.LogError(NullOrEmptyErrorMessage, nameof(input));
            return string.Empty;
        }

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            Iterations,
            Algorithm,
            KeySize);

        return string.Join(
            SegmentDelimiter,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt),
            Iterations,
            Algorithm);
    }

    public bool Verify(string input, string hashString)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(hashString))
        {
            this._logger.LogError(NullOrEmptyErrorMessage, nameof(input));
            return false;
        }

        var segments = hashString.Split(SegmentDelimiter);
        var hash = Convert.FromHexString(segments[0]);
        var salt = Convert.FromHexString(segments[1]);
        var iterations = int.Parse(segments[2]);
        var algorithm = new HashAlgorithmName(segments[3]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            iterations,
            algorithm,
            hash.Length);

        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}
