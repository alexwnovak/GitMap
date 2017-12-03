using System;

namespace GitMap
{
   public class ConfigurationReader : IConfigurationReader
   {
      public ConfigurationPair Read<T>()
      {
         if ( typeof( T ) == typeof( CommitWorkflow ) )
         {
            string filePath = @"C:\Program Files\Notepad++\notepad++.exe";
            string arguments = @"-multiInst -notabbar -nosession -noPlugin %1";

            return new ConfigurationPair( filePath, arguments );
         }

         throw new NotImplementedException();
      }
   }
}
