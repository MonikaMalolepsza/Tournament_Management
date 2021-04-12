using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament_Management.Helper
{
    public static class ClassExtensions
    {
        public static string GetYesNoString(this bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}