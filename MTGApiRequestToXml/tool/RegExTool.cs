using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.tool
{
    public class RegExTool
    {
        public RegExTool() { }
        public RegExTool(string name) { }

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
