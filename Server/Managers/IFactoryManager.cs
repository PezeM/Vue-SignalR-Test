using System;
using System.Collections.Generic;
using Server.Models;

namespace Server.Managers
{
    public interface IFactoryManager
    {
        List<Factory> Factories { get; }
        Factory GetFactory(Guid id);
        IEnumerable<Factory> GetAllFactories();
    }
}