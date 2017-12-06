namespace GitMap
{
   internal static class Program
   {
      private static int Main( string[] arguments ) => AppControllerFactory.Create().Run( arguments );
   }
}
