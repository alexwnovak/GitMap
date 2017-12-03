namespace GitMap
{
   public interface IConfigurationReader
   {
      ConfigurationPair Read<T>();
   }
}
