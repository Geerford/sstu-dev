namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a selection form of checkbox component.
    /// </summary>
    public class CheckboxItem
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Gets or sets the checked value.
        /// </summary>
        public bool IsChecked { get; set; }
    }
}