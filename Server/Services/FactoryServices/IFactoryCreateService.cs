namespace Server.Services.FactoryServices
{
    public interface IFactoryCreateService
    {
        Models.Factory CreateNewFactory(string factoryName);
    }
}