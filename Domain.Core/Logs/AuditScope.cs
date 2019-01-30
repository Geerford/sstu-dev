namespace Domain.Core.Logs
{
    /// <summary>
    /// Stores values to define an action.
    /// </summary>
    public enum AuditScope
    {
        /// <summary>
        /// Only track changes at the entity level. Limited to adding or deleting the entity.
        /// </summary>
        ClassOnly,

        /// <summary>
        /// Only track changes to property values. Limited to modifying the entity.
        /// </summary>
        PropertiesOnly,

        /// <summary>
        /// Track changes at both the entity and property levels. Including adding, deleting and modifying entities.
        /// </summary>
        ClassAndProperties
    }
}