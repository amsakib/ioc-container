using System;
using System.Collections.Generic;
using System.Text;
using IoC.Interfaces;
using IocLibrary.Attributes;

namespace IoC.Services
{
    [Register("ChapterService", typeof(IChapterService))]
    public class ChapterService : IChapterService
    {
        public void PrintAllChapters()
        {
            Console.WriteLine("I am printing all chapter data");
        }
    }
}
