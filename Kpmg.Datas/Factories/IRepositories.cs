using Kpmg.Datas.Dao.Files;

namespace Kpmg.Datas.Factories
{
    public interface IRepositories
    {
        IInformationRepository GetInformationRepository();
    }
}