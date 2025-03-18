using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.Common.Utils
{
    /// <summary>
    /// RegExTool class for handling regular expression
    /// </summary>
    public class RegExUtil
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RegExUtil() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">card name</param>
        public RegExUtil(string name) { }

        /// <summary>
        /// Format card name
        /// </summary>
        /// <param name="value">card name</param>
        /// <returns>returns Goodcardname</returns>
        public static string FormatCardName(string value)
        {
            string nonAlphasRegex = "[^\\w\\s]";

            if (string.IsNullOrEmpty(value))
                return string.Empty;
            else
            {
                string formattedName = Regex.Replace(value, nonAlphasRegex, "");
                return value.ToLower().Replace(" ", "-");
            }

        }
    }
}
