using App;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {

        private readonly Dictionary<String, int> digitValues = new()
            {
                {"N",0},
                {"I",1},
                {"V",5},
                {"X",10},
                {"L",50},
                {"C",100},
                {"D",500},
                {"M",1000},

            };
        [TestMethod]
        public void ParseTest()
        {
            Dictionary<String, int> testCases = new()
            {
                {"N",0},
                {"I",1},
                {"II",2},
                {"III",3},
                {"IIII",4}, //Цим тестом ми дозволяємо неоптимальну форму числа.
                {"IV",4},
                {"VI",6},
                {"VII",7},
                {"VIII",8},
                {"VIIII",9},
                {"IX",9},
                {"IIIIIIIIII",10},
                {"XIV",14},
                {"XXXX",40}, 
                {"XL",40},
                {"MMXXIV",2024},
                {"MMMCMXCIX",3999}


            };
            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumber.Parse(testCase.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(
                    testCase.Value,
                    rn.Value,
                    $"{testCase.Key} -> {testCase.Value}"
                );
            }

            
            Dictionary<String, Object[]> exTestCases = new()
            {
                {"W", ["W",0] },
                {"Q", ["Q",0] },
                {"s", ["s",0] },
                {"sX", ["s",0] },
                {"Xd", ["d",1] },
                {"XsI", ["s",1] },
                {"XVIs", ["s",3] },
                {"IIIIIIIIs", ["s",8] },
                {"XVIIIIIIsXVI", ["s",8] },
                {"MCMXCLY", ["Y", 6] },
                {"SWXF", ["S", 0] },
            };

            foreach(var testCase in exTestCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                () => RomanNumber.Parse(testCase.Key), $"Parse {testCase.Key} must throw FormatException");

                Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{testCase.Value[0]}' in position {testCase.Value[1]}"),
               "FormatException must contain data about symbol and its position."
               + $"testCase: '{testCase.Key}', ex.Message: '{ex.Message}'"); ;
            }

            Dictionary<String, Object[]> exTestCases2 = new()
            {
                {"IM", ["I", "M",0] },
                {"XIM", ["I", "M",1] },
                {"IMX", ["I", "M",0] },
                {"XMD", ["X", "M",0] },
                {"XID", ["X", "M",0] },
                {"IL", ["I", "L",1] },
                {"IC", ["I", "C",1] },
                {"VX", ["V", "X",0] },
                {"VL", ["V", "L",0] },
                {"DM", ["D", "M",0] },
                {"MIM", ["M", "I",2] },
                {"LC", ["L", "C",1] },
                {"XXXXIM", ["X", "I",3] },
                {"XXXXXVIM", ["X", "I",5] },
                {"LLLLLLXXXXXVIM", ["X", "I",11] },

            };

            foreach (var testCase in exTestCases2)
            {
                var ex = Assert.ThrowsException<FormatException>(
               () => RomanNumber.Parse(testCase.Key), $"Parse {testCase.Key} must throw FormatException");

                Assert.IsTrue(ex.Message.Contains($"Invalid order "),
              "FormatException must contain data about symbol and its position."
              + $"testCase: '{testCase.Key}', ex.Message: '{ex.Message}'"); ;
            }


        }

        [TestMethod]
        public void DigitValueTest()
        {
            foreach (var testCase in digitValues)
            {
                Assert.AreEqual(
                    testCase.Value,
                    RomanNumber.DigitValue(testCase.Key),
                    $"{testCase.Key} -> {testCase.Value}");
            }
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                string invalidDigit = ((char) random.Next(256)).ToString();


                if (digitValues.ContainsKey(invalidDigit))
                {
                    i--;
                    return;
                }

                ArgumentException ex2 = Assert.ThrowsException<ArgumentException>(
                   () => RomanNumber.DigitValue(invalidDigit), $"Argument Exception expected for digit = {invalidDigit}");

                Assert.IsFalse(String.IsNullOrEmpty(ex2.Message), "Argument must have a message.");

                Assert.IsTrue(
                    ex2.Message.Contains("'digit'"),
                    "ArgumentException message must contain 'digit'"
                    );

                Assert.IsTrue(
                    ex2.Message.Contains(nameof(RomanNumber))
                    && ex2.Message.Contains(nameof(RomanNumber.DigitValue)),
                    $"ArgumentException message must contain {(nameof(RomanNumber))} and {(nameof(RomanNumber.DigitValue))}'"
                    );

            }

            ArgumentException ex =  Assert.ThrowsException<ArgumentException>(
                () => RomanNumber.DigitValue(""), "Empty String Exception Expected"
            );
            // вимагитимемо від винятку 
            // - повідомлення, що не є порожнім. містить назву аргументу (digit) //містить значення аргументу, що призвело до винятку
            //назву классу та методу і що викинув виняток.

            Assert.IsFalse(String.IsNullOrEmpty( ex.Message ), "Argument must have a message.");

            Assert.IsTrue(
                ex.Message.Contains("'digit'"),
                "ArgumentException message must contain 'digit'"
                );

            Assert.IsTrue(
                ex.Message.Contains(nameof(RomanNumber)) 
                && ex.Message.Contains(nameof(RomanNumber.DigitValue)),
                $"ArgumentException message must contain {(nameof(RomanNumber))} and {(nameof(RomanNumber.DigitValue))}'"
                );
        }

        [TestMethod]
        public void ToStringTest()
        {
            Dictionary<int, String> testCases = new() //Append or Concat
            {
                {2,"II" },
                {49, "XLIX"},
                {999,"CMXCIX" },
                {444,"CDXLIV" },
                {3343,"MMMCCCXLIII" },
                {3889, "MMMDCCCLXXXIX"},
                {3954, "MMMCMLIV" },
                {4,"IV" },
                {44,"XLIV" },
                
            };

            digitValues.Keys.ToList().ForEach(k => testCases.Add(digitValues[k], k) );



            foreach (var testCase in testCases)
            {
                Assert.AreEqual(new RomanNumber(testCase.Key).ToString(),
                    testCase.Value,
                    $"ToString({testCase.Key}) ----> {testCase.Value}");
            }
        }





    }
}
