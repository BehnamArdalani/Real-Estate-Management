using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Real_Estate_Management.Business
{
    public static class Validation
    {
        public static bool IsEmail(string email)
        {
            return Regex.Match(email, @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$").Success;
        }
        public static bool IsNumberOnly(string numberOnly)
        {
            return Regex.Match(numberOnly, @"^(0*[0-9][0-9]*)$").Success;
        }
        public static bool IsDoubleOnly(string doubleOnly)
        {
            return Regex.Match(doubleOnly, @"^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$").Success;
        }
        public static bool IsPhoneNumber(string phoneNumber)
        {
            return Regex.Match(phoneNumber, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success;
        }
    }
}
