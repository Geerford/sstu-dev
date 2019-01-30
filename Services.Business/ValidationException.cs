using System;

namespace Services.Business
{
    /// <summary>
    /// Implements a validated exceptions pattern and <see cref="Exception"/>.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        public string Property { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        public ValidationException(string message, string property) : base(message)
        {
            Property = property;
        }
    }
}