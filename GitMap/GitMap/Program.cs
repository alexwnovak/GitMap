namespace GitMap
{
   internal static class Program
   {
      private static int Main( string[] arguments ) => new AppControllerFactory().Create().Run( arguments );
   }
}
