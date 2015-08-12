using System;
using Kpmg.Datas.Contexts;
using Kpmg.Datas.Dao.Files;

namespace Kpmg.Datas.Factories
{
    public class Repositories : IRepositories
    {
        private static IRepositories _soleInstance;
        private static IKpmgContext _soleInstanceContext;

        public static IInformationRepository Informations
        {
            get { return SoleInstance.GetInformationRepository(); }
        }

        private static IRepositories SoleInstance
        {
            get
            {
                if (_soleInstance == null)
                {
                    throw new InvalidOperationException("Repositories must be loaded");
                }
                return _soleInstance;
            }
        }

        private IKpmgContext SoleInstanceContext
        {
            get
            {
                if (_soleInstanceContext == null)
                {
                    throw new InvalidOperationException("Repository's Context must be loaded");
                }
                return _soleInstanceContext;
            }
        }

        public IInformationRepository GetInformationRepository()
        {
            return new InformationRepository(SoleInstanceContext);
        }

        public static void Load(IRepositories repositoryFactory)
        {
            _soleInstance = repositoryFactory;
        }

        public static void LoadContext(IKpmgContext context)
        {
            _soleInstanceContext = context;
        }

        public static void SaveChanges()
        {
            try
            {
                _soleInstanceContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}