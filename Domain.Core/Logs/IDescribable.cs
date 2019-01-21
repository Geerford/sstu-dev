namespace Domain.Core.Logs
{
    public interface IDescribable
    {
        /// <summary>
        /// Convert this object to JSON for Audit attachment
        /// </summary>
        string Describe();
    }
}