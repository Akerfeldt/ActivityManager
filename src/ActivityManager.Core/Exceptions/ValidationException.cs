using System;
using System.Collections.Generic;

namespace ActivityManager.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string> ModelErrors { get; set; }
    }
}
