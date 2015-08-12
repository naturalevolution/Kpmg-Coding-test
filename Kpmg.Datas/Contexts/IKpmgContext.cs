using System.Data.Entity;
using Kpmg.Datas.Models.Files;

namespace Kpmg.Datas.Contexts
{
    public interface IKpmgContext
    {
        IDbSet<Information> Informations { get; }
        int SaveChanges();
    }
}