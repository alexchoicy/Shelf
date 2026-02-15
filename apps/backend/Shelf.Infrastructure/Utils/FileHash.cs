

using Blake3;
using Microsoft.AspNetCore.WebUtilities;

namespace Shelf.Infrastructure.Utils
{
    public static class FileHash
    {
        public static async Task<string> GetBlake3HashAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (!stream.CanRead) throw new ArgumentException("Stream must be readable", nameof(stream));

            long? originalPosition = null;
            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                using var blake3 = new Blake3Stream(stream);

                await blake3.DrainAsync(cancellationToken).ConfigureAwait(false);

                return blake3.ComputeHash().ToString();
            }
            finally
            {
                if (originalPosition.HasValue)
                {
                    stream.Position = originalPosition.Value;
                }
            }
        }
    }
}
