using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPPTest.Shared.Utilities
{
    public static class ValidationHelper
    {
        public static bool IsValidBreedFormat(string breed)
        {
            // Regular expression pattern for valid breed format (letters only)
            // Assumes breed consists of letters (uppercase or lowercase) and optional dashes
            // Modify the pattern as needed based on your specific requirements
            string pattern = @"^[A-Za-z -]+$";

            // Use Regex.IsMatch to check if the breed matches the pattern
            return Regex.IsMatch(breed, pattern);
        }
    }
}
