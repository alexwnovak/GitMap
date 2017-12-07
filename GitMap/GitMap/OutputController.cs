using System;
using System.Reflection;

namespace GitMap
{
   public class OutputController : IOutputController
   {
      public void DisplayBanner()
      {
         var previousColor = Console.ForegroundColor;

         Console.ForegroundColor = ConsoleColor.Cyan;

         var thisAssembly = Assembly.GetExecutingAssembly();
         var assemblyName = thisAssembly.GetName();
         string version = $"{assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Build}";

         Console.WriteLine( $"{assemblyName.Name} version {version}");
         Console.ForegroundColor = previousColor;
      }
   }
}
