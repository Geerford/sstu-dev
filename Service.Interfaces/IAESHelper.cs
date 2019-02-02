using Newtonsoft.Json.Linq;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a business logic for GOST 34.12 "Grasshopper".
    /// </summary>
    public interface IAESHelper
    {
        /// <summary>
        /// Normalizes input values.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>The normalized message.</returns>
        byte[] Normalize(dynamic json);

        /// <summary>
        /// Parses the plainbytes to JSON object.
        /// </summary>
        /// <param name="plainbytes">The plainbytes.</param>
        /// <returns>The JSON object.</returns>
        JObject ParseToJson(byte[] plainbytes);
    }
}