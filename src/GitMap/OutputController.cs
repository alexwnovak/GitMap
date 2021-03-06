﻿using System;
using System.Reflection;

namespace GitMap
{
   public static class OutputController
   {
      public static void DisplayBanner()
      {
         var previousColor = Console.ForegroundColor;

         Console.ForegroundColor = ConsoleColor.Cyan;

         var thisAssembly = Assembly.GetExecutingAssembly();
         var assemblyName = thisAssembly.GetName();
         string version = $"{assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Build}";

         Console.WriteLine();
         Console.WriteLine( $"{assemblyName.Name} version {version}");
         Console.ForegroundColor = previousColor;
      }

      public static void DisplayConfigurationError( string fileName )
      {
         var previousColor = Console.ForegroundColor;

         var thisAssembly = Assembly.GetExecutingAssembly();
         var assemblyName = thisAssembly.GetName();

         Console.ForegroundColor = ConsoleColor.Red;
         Console.Write( $"[{assemblyName.Name} Error]: " );

         Console.ForegroundColor = ConsoleColor.Gray;
         Console.WriteLine( $"No editor configured for this workflow (trying to edit {fileName})" );

         Console.ForegroundColor = previousColor;
      }
   }
}
