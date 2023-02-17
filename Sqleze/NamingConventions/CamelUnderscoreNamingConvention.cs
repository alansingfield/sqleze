using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.NamingConventions;

public interface ICamelUnderscoreNamingConvention : INamingConvention { }

public class CamelUnderscoreNamingConvention : ICamelUnderscoreNamingConvention
{
    public string DotNetToSql(string arg)
    {
        arg = arg.Trim();

        StringBuilder sbResult = new StringBuilder();

        bool isNumericSave = true;
        bool firstChar = true;

        foreach(string c in
                from a in arg.ToCharArray()
                select a.ToString())
        {
            string upper = c.ToUpperInvariant();
            string lower = c.ToLowerInvariant();

            bool isNumeric = "0123456789".Contains(c);
            bool isUpper = (c == upper) && !isNumeric;

            if(!firstChar                           // Never underscore first character
                && (
                    isUpper                         // Underscore if upper case
                || (isNumeric && !isNumericSave)    // Underscore if starting a number
                )
            )
            {
                sbResult.Append("_");
            }

            sbResult.Append(lower);

            isNumericSave = isNumeric;
            firstChar = false;
        }

        return sbResult.ToString();
    }

    public string SqlToDotNet(string arg)
    {
        arg = arg.Trim();

        var lowerArg = arg.ToLowerInvariant().ToCharArray();
        var upperArg = arg.ToUpperInvariant().ToCharArray();

        StringBuilder sbResult = new StringBuilder();
        bool upperCase = true;

        for(int i = 0; i < lowerArg.Length; i++)
        {
            if(lowerArg[i] == '_')
            {
                upperCase = true;
            }
            else
            {
                sbResult.Append(upperCase ? upperArg[i] : lowerArg[i]);
                upperCase = false;
            }
        }

        return sbResult.ToString();
    }
}
