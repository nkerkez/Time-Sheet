using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.Service;

namespace TimeSheet.Tests
{
    [TestClass]
    public class CountryServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CountryService_CountryIsNull_ThrowArgumentNullException()
        {
            ICountryService countryService = new CountryService(null);
        }
    }
}
