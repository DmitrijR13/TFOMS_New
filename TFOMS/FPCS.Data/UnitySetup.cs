using System.Data.Entity;
using FPCS.Data.Migrations;
using Microsoft.Practices.Unity;

using FPCS.Core.Unity;
using FPCS.Data.Repo;
using FPCS.Data.Repo.Impl;

namespace FPCS.Data
{
    public class UnitySetup : IUnitySetup
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork<StudentManagementContext>>();

            container.RegisterType<IDbUserRepo, DbUserRepo>();
            container.RegisterType<IAdminRepo, AdminRepo>();
            container.RegisterType<ISourseIncomeRepo, SourceIncomeRepo>();
            container.RegisterType<ITypeOfAddressingRepo, TypeOfAddressingRepo>();
            container.RegisterType<IWayOfAddressingRepo, WayOfAddressingRepo>();
            container.RegisterType<IComplaintRepo, ComplaintRepo>();
            container.RegisterType<IAppealOrganizationRepo, AppealOrganizationRepo>();
            container.RegisterType<ITakingAppealLineRepo, TakingAppealLineRepo>();
            container.RegisterType<IReviewAppealLineRepo, ReviewAppealLineRepo>();
            container.RegisterType<IAppealResultRepo, AppealResultRepo>();
            container.RegisterType<IJournalAppealRepo, JournalAppealRepo>();
            container.RegisterType<IFLKRepo, FLKRepo>();
            container.RegisterType<IUserRepo, UserRepo>();
            container.RegisterType<ISMORepo, SMORepo>();
            container.RegisterType<IThemeAppealCitizensRepo, ThemeAppealCitizensRepo>();
            container.RegisterType<IHandAppealRepo, HandAppealRepo>();
            container.RegisterType<IWorkerRepo, WorkerRepo>();
            container.RegisterType<IPassedEventRepo, PassedEventRepo>();
            container.RegisterType<IMORepo, MORepo>();
            container.RegisterType<IOrganizationRepo, OrganizationRepo>();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentManagementContext, Configuration>());

            return container;
        }
    }
}
