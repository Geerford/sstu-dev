namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Creates a database backup. 
        /// </summary>
        /// <returns>The backup name.</returns>
        string Backup();

        /// <summary>
        /// Recovers database from backup.
        /// </summary>
        /// <param name="backupName">The database backup name.</param>
        bool Recovery(string backupName);

        /// <summary>
        /// Drops synchronized database.
        /// </summary>
        bool Sync();
    }
}