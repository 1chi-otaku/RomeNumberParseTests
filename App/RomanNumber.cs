using System;
using System.Text;

namespace App
{
    public record RomanNumber(int Value)
    {
        private readonly int _value = Value;
        public int Value => _value;

        public static RomanNumber Parse(String input)
        {

            int value = 0;
            int prevDigit = 0; //TODO: Rename to ~RightDigit
            int pos = input.Length;

            foreach (char c in input.Reverse())
            {
                pos -= 1;
                int digit;
                
                try 
                {
                    digit = DigitValue(c.ToString());
                }
                catch
                {
                    throw new FormatException($"Invalid symbol '{c}' in position {pos}");
                } 
                if(prevDigit != 0 && prevDigit / digit > 10)
                {
                    throw new FormatException($"Invalid order '{c}' before {input[pos+1]} in position {pos}");
                }
                value += (digit >= prevDigit) ? digit : (-digit);
                prevDigit = digit;

           }
            return new(value);
        }

        public static int DigitValue(String digit) => digit switch
        {
            "N" => 0,
            "I" => 1,
            "V" => 5,
            "X" => 10,
            "L" => 50,
            "C" => 100,
            "D" => 500,
            "M" => 1000,
            _ => throw new ArgumentException($"Message: {nameof(RomanNumber)}::{nameof(DigitValue)}: 'digit' {digit} has invalid value " )
        };

        public override string? ToString()
        {
            if (_value == 0) return "N";

            Dictionary<int, String> parts = new()
            {
                {1000, "M"},
                {900, "CM"},
                {500, "D"},
                {400, "CD"},
                {100, "C"},
                {90, "XC"},
                {50, "L"},
                {40, "XL"},
                {10, "X"},
                {9, "IX"},
                {5, "V"},
                {4, "IV"},
                {1, "I"},
            };

            int v = _value;

            StringBuilder sb = new();

            foreach(var part in parts)
            {
                while(v >= part.Key)
                {
                    v -= part.Key;
                    sb.Append(part.Value);
                }
            }
            


            return sb.ToString();
        }

    }

   
}
