using System;
using Photogram.WebApp.Models;
using Photogram.WebApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Photogram.UnitTests
{
    [TestClass]
    public class LanguageMgmtTests
    {
        [TestMethod]
        public void AcceptGoodParam()
        {
            CultureManagement.SetCulture("en-US");
        }

        [TestMethod]
        [ExpectedException(typeof(CultureNotFoundException))]
        public void HandleBadParam()
        {
            CultureManagement.SetCulture("argargar");
        }

        [TestMethod]
        [ExpectedException(typeof(CultureNotFoundException))]
        public void ZeroIntParam()
        {
            int i = 0;
            CultureManagement.SetCulture(i);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullStrParam()
        {
            string str = null;
            CultureManagement.SetCulture(str);
        }
    }

    [TestClass]
    public class BlogTests
    {
        [TestMethod]
        public void EmptyPost()
        {
            var ctrl = new BlogAdminController();

            var result = ctrl.Edit(new BlogPostInformation());
            dynamic dresult = result.Data;

            Assert.AreEqual(true, dresult.Success);
        }
    }
}
