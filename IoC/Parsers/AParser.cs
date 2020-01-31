using System;
using System.Collections.Generic;
using System.Text;
using IoC.Interfaces;
using IocLibrary.Attributes;

namespace IoC.Parsers
{
    [Register("AParser", typeof(IParser))]
    public class AParser : IParser
    {
        public void Parse(string text)
        {
            Console.WriteLine("A parser received text: "+ text);
        }
    }
}
