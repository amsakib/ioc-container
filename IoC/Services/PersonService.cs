using System;
using System.Collections.Generic;
using System.Text;
using IoC.Interfaces;
using IocLibrary.Attributes;

namespace IoC.Services
{
    [Register("PersonService", typeof(IPersonService))]
    public class PersonService : IPersonService
    {
        public void PrintAllPersons()
        {
            Console.WriteLine("I am printing all persons.");
        }
    }
}
