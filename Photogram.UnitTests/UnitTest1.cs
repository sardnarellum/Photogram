using System;
using Photogram.WebApp.Models;
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
}
