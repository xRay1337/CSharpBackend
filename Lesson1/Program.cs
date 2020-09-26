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
            var url = "DEBUG";
#else
            var url = ConfigurationManager.AppSettings["SiteUrl"];
#endif
            Helper.WriteText(url);

#region ReadLine
            Console.ReadLine();
#endregion
        }
    }
}