using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {


        [TestMethod]
        public void Parse_I_ReturnsOne()
        {
            RomanNumber rn = RomanNumber.Parse("I");
            Assert.AreEqual(1, rn.Value, "I should return 1.");
        }

        [TestMethod]
        public void Parse_IV_ReturnsFour()
        {
            RomanNumber rn = RomanNumber.Parse("IV");
            Assert.AreEqual(4, rn.Value, "IV should return 4.");
        }

        [TestMethod]
        public void Parse_V_ReturnsFive()
        {
            RomanNumber rn = RomanNumber.Parse("V");
            Assert.AreEqual(5, rn.Value, "V should return 5.");
        }

        [TestMethod]
        public void Parse_IX_ReturnsNine()
        {
            RomanNumber rn = RomanNumber.Parse("IX");
            Assert.AreEqual(9, rn.Value, "IX should return 9.");
        }

        [TestMethod]
        public void Parse_X_ReturnsTen()
        {
            RomanNumber rn = RomanNumber.Parse("X");
            Assert.AreEqual(10, rn.Value, "X should return 10.");
        }

    }
}
