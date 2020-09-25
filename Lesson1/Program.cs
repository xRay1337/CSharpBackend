using LibForLesson1;
using System;
using System.Configuration;

namespace Lesson1
{
    class Program
    {
        //Просто текст
        static void Main(string[] args)
        {
#if DEBUG            
            var Url = "DEBUG";
#else
            var Url = ConfigurationManager.AppSettings["SiteUrl"];
#endif
            Helper.WriteText(Url);

#region ReadLine
            Console.ReadLine();
#endregion
        }
    }
}