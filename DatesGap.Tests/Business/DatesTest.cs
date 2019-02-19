using DatesGap.Business;
using DatesGap.Models;
using NUnit.Framework;
using System;
using System.Web.Mvc;

namespace DatesGap.Tests.Business
{
    [TestFixture()]
    public class DatesTest
    {
        [Test()]
        public void Gap()
        {
            // Arrange
            Date fromDate = new Date()
            {
                Month = 11,
                Day = 22,
                Year = 2003
            };
            Date toDate = new Date()
            {
                Month = 3,
                Day = 21,
                Year = 2028
            };

            // Act
            var result = Dates.Gap(fromDate, toDate);

            // Assert
            Assert.AreEqual(8886, result);

            // Arrange
            fromDate.Month = 2;
            fromDate.Day = 22;
            fromDate.Year = 2022;
            toDate.Month = 2;
            toDate.Day = 2;
            toDate.Year = 2022;

            // Act
            result = Dates.Gap(fromDate, toDate);

            // Assert
            Assert.AreEqual(0, result);

            // Arrange
            fromDate.Month = 2;
            fromDate.Day = 14;
            fromDate.Year = 2012;
            toDate.Month = 2;
            toDate.Day = 16;
            toDate.Year = 2012;

            // Act
            result = Dates.Gap(fromDate, toDate);

            // Assert
            Assert.AreEqual(2, result);

            // Arrange
            fromDate.Month = 2;
            fromDate.Day = 19;
            fromDate.Year = 2012;
            toDate.Month = 3;
            toDate.Day = 8;
            toDate.Year = 2012;

            // Act
            result = Dates.Gap(fromDate, toDate);

            // Assert
            Assert.AreEqual(18, result);

            // Arrange
            fromDate.Month = 12;
            fromDate.Day = 31;
            fromDate.Year = 2008;
            toDate.Month = 1;
            toDate.Day = 1;
            toDate.Year = 2009;

            // Act
            result = Dates.Gap(fromDate, toDate);

            // Assert
            Assert.AreEqual(1, result);

            // Arrange
            fromDate.Month = 12;
            fromDate.Day = 31;
            fromDate.Year = 2019;
            toDate.Month = 12;
            toDate.Day = 31;
            toDate.Year = 2020;

            // Act
            result = Dates.Gap(fromDate, toDate);

            // Assert
            Assert.AreEqual(366, result);
        }

        [Test()]
        public void IsLeapYear()
        {
            // Arrange
            short year = 2019;

            // Act
            var result = Dates.IsLeapYear(year);

            // Assert
            Assert.AreEqual(false, result);

            // Arrange
            year = 2020;

            // Act
            result = Dates.IsLeapYear(year);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
