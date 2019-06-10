using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ilkyar.Business.BusinessRules.Account;
using Ilkyar.Business.BusinessRules.Message;
using Ilkyar.Business.BusinessRules.Parameter;
using Ilkyar.Business.BusinessRules.Profile;
using Ilkyar.Business.BusinessRules.Project;
using Ilkyar.Business.BusinessRules.Report;
using Ilkyar.Business.BusinessRules.User;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.ServiceContracts.Account;
using Ilkyar.Contracts.ServiceContracts.Message;
using Ilkyar.Contracts.ServiceContracts.Parameter;
using Ilkyar.Contracts.ServiceContracts.Profile;
using Ilkyar.Contracts.ServiceContracts.Project;
using Ilkyar.Contracts.ServiceContracts.Report;
using Ilkyar.Contracts.ServiceContracts.User;
using Ilkyar.Contracts.UnitOfWork;
using Ilkyar.Infrastructure.Repositories;
using Ilkyar.Infrastructure.UnitOfWork;

namespace Ilkyar.WebAPI.Installers
{
    public class Bootstrapper : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<EFUnitOfWork>().LifestylePerWebRequest());
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(EFRepository<>)).LifestylePerWebRequest());

            container.Register(Component.For<IAccount>().ImplementedBy<AccountBusiness>().LifestylePerWebRequest());
            container.Register(Component.For<IParameter>().ImplementedBy<ParameterBusiness>().LifestylePerWebRequest());
            container.Register(Component.For<IProfile>().ImplementedBy<ProfileBusiness>().LifestylePerWebRequest());
            container.Register(Component.For<IProject>().ImplementedBy<ProjectBusiness>().LifestylePerWebRequest());
            container.Register(Component.For<IReport>().ImplementedBy<ReportBusiness>().LifestylePerWebRequest());
            container.Register(Component.For<IUser>().ImplementedBy<UserBusiness>().LifestylePerWebRequest());
            container.Register(Component.For<IMessage>().ImplementedBy<MessageBusiness>().LifestylePerWebRequest());
        }
    }
}