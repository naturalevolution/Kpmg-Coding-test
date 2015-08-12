using System.Data.Entity;
using Kpmg.Datas.Contexts;
using Kpmg.Datas.Models.Files;

namespace Kpmg.Datas.Dao.Files
{
    public class InformationRepository : BaseRepository<Information>, IInformationRepository
    {
        public InformationRepository(IKpmgContext context)
            : base(context)
        {
        }

        protected override IDbSet<Information> GetDbSet()
        {
            return CurrentContext.Informations;
        }
    }
}