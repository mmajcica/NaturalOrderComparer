using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace NaturalSortOrder
{
    /// <summary>
    /// Compares two objects for equivalence, following the natural sort order.
    /// </summary>
    /// <remarks>NaturalOrderComparer implements the IComparer interface supporting 
    /// natural order comparisons of strings.</remarks>
    [Serializable]
    public sealed class NaturalOrderComparer : IComparer, IComparer<string>
    {
        private readonly CultureInfo m_cultureInfo;
        private readonly bool m_ignoreGroupSeparator;
        private readonly bool m_ignoreDecimalSeprator;

        #region Private Constructors
        /// <summary>
        /// Initializes a new instance of the NaturalOrderComparer class using 
        /// the <see cref="System.Threading.Thread.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <param name="ignoreDecimalSeprator">Ignores the decimal separator and treats numbers as integers.</param>
        /// <param name="ignoreGroupSeparator">Ignores the group separator when analyzing the string.</param>
        /// <remarks>
        /// When the <see cref="NaturalOrderComparer"/> instance is created 
        /// using this constructor, the <see cref="System.Threading.Thread.CurrentCulture"/> of the 
        /// current thread is saved. Comparison procedures use the saved culture to 
        /// determine the sort order and casing rules; therefore, comparisons might 
        /// have different results depending on the culture.</remarks>
        private NaturalOrderComparer(bool ignoreDecimalSeprator, bool ignoreGroupSeparator)
            : this(CultureInfo.CurrentCulture, ignoreDecimalSeprator, ignoreGroupSeparator)
        { }

        /// <summary>
        /// Initializes a new instance of the NaturalOrderComparer class 
        /// using the specified <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use for 
        /// the new <see cref="NaturalOrderComparer"/>.</param>
        /// <param name="ignoreDecimalSeprator">Ignores the decimal separator and treats numbers as integers.</param>
        /// <param name="ignoreGroupSeparator">Ignores the group separator when analyzing the string.</param>
        /// <remarks>
        /// Comparison procedures use the specified <see cref="System.Globalization.CultureInfo"/> to 
        /// determine the sort order. Comparisons might have different results depending on the culture.
        /// </remarks>
        private NaturalOrderComparer(CultureInfo culture, bool ignoreDecimalSeprator, bool ignoreGroupSeparator)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            this.m_cultureInfo = culture;
            this.m_ignoreDecimalSeprator = ignoreDecimalSeprator;
            this.m_ignoreGroupSeparator = ignoreGroupSeparator;
        } 
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the NaturalOrderComparer class using 
        /// the <see cref="System.Threading.Thread.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <remarks>
        /// When the <see cref="NaturalOrderComparer"/> instance is created 
        /// using the default constructor, the <see cref="System.Threading.Thread.CurrentCulture"/> of the 
        /// current thread is saved. Comparison procedures use the saved culture to 
        /// determine the sort order and casing rules; therefore, comparisons might 
        /// have different results depending on the culture.</remarks>
        public NaturalOrderComparer()
            : this(CultureInfo.CurrentCulture, false, false)
        { }

        /// <summary>
        /// Initializes a new instance of the NaturalOrderComparer class using 
        /// the <see cref="System.Threading.Thread.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <param name="ignoreDecimalSeprator">Ignores the decimal separator when analyzing the string.</param>
        /// <remarks>
        /// When the <see cref="NaturalOrderComparer"/> instance is created 
        /// using this constructor, the <see cref="System.Threading.Thread.CurrentCulture"/> of the 
        /// current thread is saved. Comparison procedures use the saved culture to 
        /// determine the sort order and casing rules; therefore, comparisons might 
        /// have different results depending on the culture.</remarks>
        public NaturalOrderComparer(bool ignoreDecimalSeprator)
            : this(ignoreDecimalSeprator, true)
        { }

        /// <summary>
        /// Initializes a new instance of the NaturalOrderComparer class using 
        /// the <see cref="System.Threading.Thread.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <param name="decimalSeprator">Character that will be used as decimal separator.</param>
        /// <remarks>
        /// When the <see cref="NaturalOrderComparer"/> instance is created 
        /// using this constructor, the <see cref="System.Threading.Thread.CurrentCulture"/> of the 
        /// current thread is saved. Comparison procedures use the saved culture to 
        /// determine the sort order and casing rules; therefore, comparisons might 
        /// have different results depending on the culture.</remarks>
        public NaturalOrderComparer(string decimalSeprator)
        {
            if (string.IsNullOrEmpty(decimalSeprator))
            {
                throw new ArgumentNullException("decimalSeprator");
            }

            CultureInfo customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = decimalSeprator;

            this.m_ignoreDecimalSeprator = false;
            this.m_ignoreGroupSeparator = true;

            this.m_cultureInfo = customCulture;
        }

        /// <summary>
        /// Initializes a new instance of the NaturalOrderComparer class using 
        /// the <see cref="System.Threading.Thread.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <param name="decimalSeprator">Character that will be used as decimal separator.</param>
        /// <param name="groupSeparator">Character that will be used as group separator.</param>
        /// <remarks>
        /// When the <see cref="NaturalOrderComparer"/> instance is created 
        /// using this constructor, the <see cref="System.Threading.Thread.CurrentCulture"/> of the 
        /// current thread is saved. Comparison procedures use the saved culture to 
        /// determine the sort order and casing rules; therefore, comparisons might 
        /// have different results depending on the culture.</remarks>
        public NaturalOrderComparer(string decimalSeprator, string groupSeparator)
        {
            if (string.IsNullOrEmpty(decimalSeprator))
            {
                throw new ArgumentNullException("decimalSeprator");
            }

            if (string.IsNullOrEmpty(groupSeparator))
            {
                throw new ArgumentNullException("groupSeparator");
            }

            if (decimalSeprator.Length > 1)
            {
                throw new NotSupportedException("Decimal separator must be at maximum 1 character long.");
            }

            if (groupSeparator.Length > 1)
            {
                throw new NotSupportedException("Group separator must be at maximum 1 character long.");
            }

            if (decimalSeprator == groupSeparator)
            {
                throw new ArgumentException("Decimal and Group separator can't be the same character.");
            }

            CultureInfo customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = decimalSeprator;
            customCulture.NumberFormat.NumberGroupSeparator = groupSeparator;

            this.m_ignoreDecimalSeprator = false;
            this.m_ignoreGroupSeparator = false;

            this.m_cultureInfo = customCulture;
        }

        /// <summary>
        /// Initializes a new instance of the NaturalOrderComparer class 
        /// using the specified <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use for 
        /// the new <see cref="NaturalOrderComparer"/>.</param>
        /// <remarks>
        /// Comparison procedures use the specified <see cref="System.Globalization.CultureInfo"/> to 
        /// determine the sort order. Comparisons might have different results depending on the culture.
        /// </remarks>
        public NaturalOrderComparer(CultureInfo culture)
            : this(culture, false, false)
        { }
        #endregion

        #region Properties
        /// <summary>
        /// Gets an instance of NaturalOrderComparer that is associated with 
        /// the <see cref="System.Threading.Thread.CurrentCulture"/> of the current thread.
        /// </summary>
        public static NaturalOrderComparer CurrentCulture
        {
            get
            {
                return new NaturalOrderComparer();
            }
        }

        /// <summary>
        /// Gets an instance of NaturalOrderComparer that is associated 
        /// with <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        public static NaturalOrderComparer InvariantCulture
        {
            get
            {
                return new NaturalOrderComparer(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets an instance of NaturalOrderComparer that ignores separators 
        /// and treat all numbers as integers.
        /// </summary>
        public static NaturalOrderComparer DefaultInteger
        {
            get
            {
                return new NaturalOrderComparer(true);
            }
        } 
        #endregion

        /// <summary>
        /// Performs a natural order comparison of two objects of the same type and 
        /// returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of a and b, as follows:
        /// Less than zero - a is less than b, by natural order
        /// Zero - a equals b, by natural order
        /// Greater than zero - a is greater than b, by natural order</returns>
        public int Compare(object x, object y)
        {
            string left = x as string;
            string right = y as string;

            return this.Compare(left, right);
        }

        /// <summary>
        /// Performs a natural order comparison of two strings and 
        /// returns a value indicating whether one is less than, equal to, or greater than the other.
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

            int lenLeft = x.Length;
            int lenRight = y.Length;
            int posLeft = 0;
            int posRight = 0;
            string NumberDecimalSeparator = this.m_cultureInfo.NumberFormat.NumberDecimalSeparator;

            // repeat until the position marker is lower
            // than the length of analyzing string
            while (posLeft < lenLeft && posRight < lenRight)
            {
                char currCharLeft = x[posLeft];
                char currCharRight = y[posRight];

                char[] bufferLeft = new char[lenLeft];
                char[] bufferRight = new char[lenRight];
                int bufferPosLeft = 0;
                int bufferPosRight = 0;

                // create a buffer, read the string char by char
                // and check if the next char should be a part of the buffer
                do
                {
                    bufferLeft[bufferPosLeft++] = currCharLeft;
                    posLeft++;

                    if (posLeft < lenLeft)
                    {
                        currCharLeft = x[posLeft];
                    }
                    else
                    {
                        break;
                    }
                } while (ShouldContinueReading(currCharLeft, bufferLeft));

                do
                {
                    bufferRight[bufferPosRight++] = currCharRight;
                    posRight++;

                    if (posRight < lenRight)
                    {
                        currCharRight = y[posRight];
                    }
                    else
                    {
                        break;
                    }
                } while (ShouldContinueReading(currCharRight, bufferRight));

                string leftString = new string(bufferLeft);
                string rightString = new string(bufferRight);
                int result;

                //// if the first char is a decimal separator, add leading zero in 
                //// order to allow a proper parsing of the string
                //if (!m_ignoreDecimalSeprator)
                //{
                //    if (bufferLeft[0].Equals(NumberDecimalSeparator[0]) && char.IsDigit(bufferLeft[1]))
                //        leftString = "0" + leftString;

                //    if (bufferRight[0].Equals(NumberDecimalSeparator[0]) && char.IsDigit(bufferRight[1]))
                //        rightString = "0" + rightString;
                //}

                // If both strings starts with a digit, compare as digits
                // otherwise compare as strings
                if (char.IsDigit(leftString[0]) && char.IsDigit(rightString[0]))
                {
                    decimal chunkLeft = decimal.Parse(leftString, m_cultureInfo);
                    decimal chunkRight = decimal.Parse(rightString, m_cultureInfo);

                    result = chunkLeft.CompareTo(chunkRight);
                }
                else
                {
                    result = m_cultureInfo.CompareInfo.Compare(leftString, rightString);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return lenLeft - lenRight;
        }

        /// <summary>
        /// Checks to see if the current character should be added into the buffer.
        /// </summary>
        /// <param name="currentChar">Current character. (is never the first character in the buffer)</param>
        /// <param name="buffer">Current buffer.</param>
        /// <returns>True if the current character should be added into the current buffer, otherwise false.</returns>
        private bool ShouldContinueReading(char currentChar, char[] buffer)
        {
            char NumberDecimalSeparator = char.Parse(this.m_cultureInfo.NumberFormat.NumberDecimalSeparator);
            char NumberGroupSeparator = char.Parse(this.m_cultureInfo.NumberFormat.NumberGroupSeparator);
            bool CurrentCharIsDigit = char.IsDigit(currentChar);
            bool BufferZeroIsDigit = char.IsDigit(buffer[0]);

            if ((!CurrentCharIsDigit && !BufferZeroIsDigit)
              || (CurrentCharIsDigit && BufferZeroIsDigit))
                return true;

            if (BufferZeroIsDigit && !CurrentCharIsDigit)
            {
                if (!this.m_ignoreDecimalSeprator && NumberDecimalSeparator.Equals(currentChar) &&
                        !Array.Exists(buffer, delegate(char c) { return c == NumberDecimalSeparator; }))
                    return true;

                if (!this.m_ignoreGroupSeparator && NumberGroupSeparator.Equals(currentChar))
                    return true;
            }

            return false;
        }
    }
}
