namespace EasyRent.Configuration;

public interface IConfigurationProvider<TConfiguration>
    where TConfiguration : class
{
    TConfiguration GetConfiguration();
}