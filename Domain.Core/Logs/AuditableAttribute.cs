using System;

namespace Domain.Core.Logs
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuditableAttribute : Attribute
    {
        public AuditableAttribute(AuditScope auditScope)
        {
            AuditScope = auditScope;
        }

        public AuditScope AuditScope { get; } = AuditScope.ClassOnly;
    }
}