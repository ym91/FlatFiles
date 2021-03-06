﻿using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace FlatFiles.Test
{

    /// <summary>
    /// Tests the DateTimeColumn class.
    /// </summary>
    public class DateTimeColumnTester
    {
        public DateTimeColumnTester()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        /// <summary>
        /// An exception should be thrown if name is blank.
        /// </summary>
        [Fact]
        public void TestCtor_NameBlank_Throws()
        {
            Assert.Throws<ArgumentException>(() => new DateTimeColumn("    "));
        }

        /// <summary>
        /// If someone tries to pass a name that contains leading or trailing whitespace, it will be trimmed.
        /// The name will also be made lower case.
        /// </summary>
        [Fact]
        public void TestCtor_SetsName_Trimmed()
        {
            DateTimeColumn column = new DateTimeColumn(" Name   ");
            Assert.Equal("Name", column.ColumnName);
        }

        /// <summary>
        /// If no format string is provided, a generic parse will be attempted.
        /// </summary>
        [Fact]
        public void TestParse_NoFormatString_ParsesGenerically()
        {
            DateTimeColumn column = new DateTimeColumn("created");
            DateTime actual = (DateTime)column.Parse("1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// If no format string is provided, a generic parse will be attempted.
        /// </summary>
        [Fact]
        public void TestParse_FormatProvider_NoFormatString_ParsesGenerically()
        {
            DateTimeColumn column = new DateTimeColumn("created");
            column.FormatProvider = CultureInfo.CurrentCulture;
            DateTime actual = (DateTime)column.Parse("1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// If no format string is provided, an exact parse will be attempted.
        /// </summary>
        [Fact]
        public void TestParse_FormatString_ParsesExactly()
        {
            DateTimeColumn column = new DateTimeColumn("created");
            column.InputFormat = "d";
            DateTime actual = (DateTime)column.Parse("1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// If no format string is provided, an exact parse will be attempted.
        /// </summary>
        [Fact]
        public void TestParse_FormatProvider_FormatString_ParsesExactly()
        {
            DateTimeColumn column = new DateTimeColumn("created");
            column.InputFormat = "d";
            column.FormatProvider = CultureInfo.CurrentCulture;
            DateTime actual = (DateTime)column.Parse("1/19/2013");
            DateTime expected = new DateTime(2013, 1, 19);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// If the value is blank and the field is not required, null will be returned.
        /// </summary>
        [Fact]
        public void TestParse_ValueBlank_NullReturned()
        {
            DateTimeColumn column = new DateTimeColumn("created");
            DateTime? actual = (DateTime?)column.Parse("    ");
            DateTime? expected = null;
            Assert.Equal(expected, actual);
        }
    }
}
