namespace Domain.Core.Logs
{
    /// <summary>
    /// Provides method that converts {T} object to JSON format.
    /// </summary>
    public interface IDescribable
    {
        /// <summary>
        /// Converts this object to JSON for <see cref="Audit"/> attachment.
        /// </summary>
        string Describe();
    }
}