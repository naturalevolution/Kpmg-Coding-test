using System.Data.Entity;
using Kpmg.Datas.Contexts;
using Kpmg.Datas.Contexts.Fakes;
using Kpmg.Datas.Models.Files;

namespace Kpmg.Tests.Datas.Memory
{
    public class FakeKpmgContext : IKpmgContext
    {
        public FakeKpmgContext()
        {
            Informations = new MemoryDbSet<Information>();
        }

        public IDbSet<Information> Informations { get; private set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}