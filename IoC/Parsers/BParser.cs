using System;
using System.Collections.Generic;
using System.Text;
using IoC.Interfaces;
using IocLibrary.Attributes;

namespace IoC.Parsers
{
    [Register("BParser", typeof(IParser))]
    public class BParser : IParser
    {
        public void Parse(string text)
        {
            Console.WriteLine("B Parser received text: " + text);
        }
    }
}
