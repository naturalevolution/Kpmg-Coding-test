using System.Data.Entity;
using Kpmg.Datas.Contexts.Fakes;
using Kpmg.Datas.Models.Files;

namespace Kpmg.Datas.Contexts
{
    public class KpmgContext : DbContext, IKpmgContext
    {
        public KpmgContext()
        {
            Informations = new MemoryDbSet<Information>();
        }

        public IDbSet<Information> Informations { get; set; }
    }
}