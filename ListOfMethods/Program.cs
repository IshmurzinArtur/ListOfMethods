using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ListOfMethods
{
    class Program
    {
        public static void GetInfo(string pathDir)
        {
            var dir = new DirectoryInfo(pathDir);
            List<Assembly> assemblies = new List<Assembly>();
            foreach (FileInfo fileInfo in dir.GetFiles("*.dll"))
            {
                assemblies.Add(Assembly.LoadFrom(fileInfo.FullName));
            }
            foreach (var assembly in assemblies)
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        Console.WriteLine(type.Name);
                        foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                        {
                            if (method.IsFamily || method.IsPublic) //исключаем private
                                Console.WriteLine("      " + method.Name);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            string pathDir = @"C:\Users\Artur\source\repos\ListOfMethods\ListOfMethods\assembly";
            GetInfo(pathDir);
        }
    }
}
