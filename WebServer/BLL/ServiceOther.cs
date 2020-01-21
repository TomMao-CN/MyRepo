using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceOther:SuperClass
    {
        public string GetTemperature(string input)
        {
            string pattern = @"-?[\d]+[.]?[\d]*";
            Match match = Regex.Match(input, pattern);
            return match.Value;
        }
    }
}
