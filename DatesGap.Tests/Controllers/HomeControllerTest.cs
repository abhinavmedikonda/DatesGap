using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DatesGap;
using DatesGap.Controllers;
using DatesGap.Models;

namespace DatesGap.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = (ViewResult)controller.Index();

            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            var expectedVersion = mvcName.Version.Major + "." + mvcName.Version.Minor;
            var expectedRuntime = isMono ? "Mono" : ".NET";

            // Assert
            Assert.AreEqual(expectedVersion, result.ViewData["Version"]);
            Assert.AreEqual(expectedRuntime, result.ViewData["Runtime"]);
        }

        [Test]
        public void Submit()
        {
            // Arrange
            var controller = new HomeController();
            HomeVM homeVM = new HomeVM()
            {
                FromDate = "12/11/2000",
                ToDate = "11/12/2022"
            };

            // Act
            var result = (ViewResult)controller.Submit(homeVM);
            var model = result.Model as HomeVM;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(8006, model.Gap);
        }
    }
}
