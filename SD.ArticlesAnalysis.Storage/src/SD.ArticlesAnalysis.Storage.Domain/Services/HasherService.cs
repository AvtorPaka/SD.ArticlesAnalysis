using System.Security.Cryptography;
using SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;

namespace SD.ArticlesAnalysis.Storage.Domain.Services;

public class HasherService : IHasherService
{
    public string ComputeFileHash(Stream fileStream)
    {
        long initialPosition = 0;
        bool canSeek = fileStream.CanSeek;

        try
        {
            if (canSeek)
            {
                initialPosition = fileStream.Position;
                fileStream.Position = 0;
            }

            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                byte[] hashBytes = hashAlgorithm.ComputeHash(fileStream);
                return BitConverter.ToString(hashBytes)
                    .Replace("-", "")
                    .ToLowerInvariant();
            }
        }
        finally
        {
            if (canSeek)
            {
                fileStream.Position = initialPosition;
            }
        }
    }
}