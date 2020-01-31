using System;
using System.Reflection;
using IoC.Services;
using IocLibrary;
using IocLibrary.Attributes;
using IocLibrary.Container;

namespace IoC
{
    public class Program
    {
        static void Main(string[] args) { 
            // register all types
            IocContainer.Instance.AddAssembly(Assembly.GetExecutingAssembly());
            // get a instance of ExampleService
            var exampleService = IocContainer.Instance.GetInstance<ExampleService>();
            exampleService.TestExecution();
        }
    }
}
