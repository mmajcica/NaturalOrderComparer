using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace NaturalSortComparer
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
    }


    /// <summary>
    /// Compares two Unicode strings. 
    /// Digits in the strings are considered as numerical content rather than text.
    /// </summary>
    public sealed class NaturalOrderComparerNative : IComparer, IComparer<string>
    {
        /// <summary>
        /// Represents an instance of <see cref="NaturalOrderComparerNative"/>.
        /// </summary>
        public static NaturalOrderComparerNative Default
        {
            get
            {
                return new NaturalOrderComparerNative();
            }
        }

        /// <summary>
        /// Compares two Unicode strings. 
        /// Digits in the strings are considered as numerical content rather than text.
        /// </summary>
        /// <param name="x">The first string to compare.</param>
        /// <param name="y">The second string to compare.</param>
        /// <returns>A signed integer that indicates the relative values of a and b, as follows:
        /// Less than zero - a is less than b, by natural order
        /// Zero - a equals b, by natural order
        /// Greater than zero - a is greater than b, by natural order</returns>
        public int Compare(string x, string y)
        {
            if (x == null || y == null)
            {
                return Comparer.Default.Compare(x, y);
            }

            return SafeNativeMethods.StrCmpLogicalW(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of a and b, as follows:
        /// Less than zero - a is less than b, by natural order
        /// Zero - a equals b, by natural order
        /// Greater than zero - a is greater than b, by natural order</returns>
        public int Compare(object x, object y)
        {
            return Compare(x as string, y as string);
        }
    }
}
