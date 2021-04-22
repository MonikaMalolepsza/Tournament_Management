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
        public static bool GetTrueFalseString(this string value)
        {
            switch (value.ToLower())
            {
                case "yes":
                    return true;
                case "no":
                    return false;
                default: throw new Exception("Invalid Input!");
            }
        }
    }
}