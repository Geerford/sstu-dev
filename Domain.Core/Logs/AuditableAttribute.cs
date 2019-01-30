using System;

namespace Domain.Core.Logs
{
    /// <summary>
    /// Represents a custom attribute for auditing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AuditableAttribute : Attribute
    {
        /// <summary>
        /// Gets the audit scope.
        /// </summary>
        public AuditScope AuditScope { get; } = AuditScope.ClassOnly;

        /// <summary>
        /// Initializes a <see cref="AuditableAttribute"/> class.
        /// </summary>
        /// <param name="auditScope">The audit scope.</param>
        public AuditableAttribute(AuditScope auditScope)
        {
            AuditScope = auditScope;
        }
    }
}