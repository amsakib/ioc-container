using System;
using System.Collections.Generic;
using System.Text;
using IoC.Interfaces;
using IocLibrary.Attributes;
using IocLibrary.Container;

namespace IoC.Services
{
    [Register]
    public class ExampleService
    {
        [Inject]
        public IChapterService ChapterService { get; set; }
        [Inject]
        public IPersonService PersonService { get; set; }

        public void TestExecution()
        {
            Console.WriteLine("Lets print chapter data.");
            ChapterService.PrintAllChapters();
            Console.WriteLine("Lets print person data.");
            PersonService.PrintAllPersons();
            Console.WriteLine("Lets get all parsers");
            var parsers = IocContainer.Instance.GetInstances<IParser>();
            Console.WriteLine($"Total {parsers.Count} parser(s) found.");
            foreach (var parser in parsers)
            {
                parser.Parse("Text");
            }
        }
    }
}
