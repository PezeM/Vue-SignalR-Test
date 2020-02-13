using System;
using Server.Models;

namespace Server.Services.FactoryServices
{
    public class FactoryCreateService : IFactoryCreateService
    {
        public Factory CreateNewFactory(string factoryName)
        {
            return new Factory(Guid.NewGuid(), factoryName);
        }
    }
}