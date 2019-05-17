using System.Collections.Generic;

namespace Infrastructure.Data.Utilities
{
    /// <summary>
    /// Utility for binding cyrillic to latin.
    /// </summary>
    internal class Translator
    {
        /// <summary>
        /// The linked table.
        /// </summary>
        private readonly Dictionary<string, string> table = new Dictionary<string, string>
            {
                { "а", "a" },
                { "б", "b" },
                { "в", "v" },
                { "г", "g" },
                { "д", "d" },
                { "е", "e" },
                { "ё", "yo" },
                { "ж", "zh" },
                { "з", "z" },
                { "и", "i" },
                { "й", "j" },
                { "к", "k" },
                { "л", "l" },
                { "м", "m" },
                { "н", "n" },
                { "о", "o" },
                { "п", "p" },
                { "р", "r" },
                { "с", "s" },
                { "т", "t" },
                { "у", "u" },
                { "ф", "f" },
                { "х", "h" },
                { "ц", "c" },
                { "ч", "ch" },
                { "ш", "sh" },
                { "щ", "sch" },
                { "ъ", "j" },
                { "ы", "i" },
                { "ь", "j" },
                { "э", "e" },
                { "ю", "yu" },
                { "я", "ya" },
                { "А", "A" },
                { "Б", "B" },
                { "В", "V" },
                { "Г", "G" },
                { "Д", "D" },
                { "Е", "E" },
                { "Ё", "Yo" },
                { "Ж", "Zh" },
                { "З", "Z" },
                { "И", "I" },
                { "Й", "J" },
                { "К", "K" },
                { "Л", "L" },
                { "М", "M" },
                { "Н", "N" },
                { "О", "O" },
                { "П", "P" },
                { "Р", "R" },
                { "С", "S" },
                { "Т", "T" },
                { "У", "U" },
                { "Ф", "F" },
                { "Х", "H" },
                { "Ц", "C" },
                { "Ч", "Ch" },
                { "Ш", "Sh" },
                { "Щ", "Sch" },
                { "Ъ", "J" },
                { "Ы", "I" },
                { "Ь", "J" },
                { "Э", "E" },
                { "Ю", "Yu" },
                { "Я", "Ya" }
            };

        /// <summary>
        /// Binds the message from cyrillic to latin.
        /// </summary>
        /// <param name="message">The cyrillic message.</param>
        /// <returns>The latin message.</returns>
        public string Bind(string message)
        {
            foreach (KeyValuePair<string, string> pair in table)
            {
                message = message.Replace(pair.Key, pair.Value);
            }
            return message;
        }
    }
}