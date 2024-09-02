using System;

namespace App
{
    public record RomanNumber(int Value)
    {
        private readonly int _value = Value;
        public int Value => _value;

        public static RomanNumber Parse(string input)
        {
            input = input.ToUpper();

            return input switch
            {
                "I" => new RomanNumber(1),
                "IV" => new RomanNumber(4),
                "V" => new RomanNumber(5),
                "IX" => new RomanNumber(9),
                "X" => new RomanNumber(10),
                _ => throw new ArgumentException("Invalid Number!"),
            };
        }
    }
}
