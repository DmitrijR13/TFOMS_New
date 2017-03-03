using System.Data.Entity;

using FPCS.Data.Cofigs;

namespace FPCS.Data
{
    internal class StudentManagementContext : DbContext
    {
        public StudentManagementContext()
            : base("ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DbUserConfig());
            modelBuilder.Configurations.Add(new AdminConfig());
            modelBuilder.Configurations.Add(new SourceIncomeConfig());
            modelBuilder.Configurations.Add(new WayOfAddressingConfig());
            modelBuilder.Configurations.Add(new TypeOfAddressingConfig());
            modelBuilder.Configurations.Add(new AppealOrganizationConfig());
            modelBuilder.Configurations.Add(new TakingAppealLineConfig());
            modelBuilder.Configurations.Add(new ReviewAppealLineConfig());
            modelBuilder.Configurations.Add(new AppealResultConfig());
            modelBuilder.Configurations.Add(new ThemeAppealCitizensConfig());
            modelBuilder.Configurations.Add(new JournalAppealConfig());
            modelBuilder.Configurations.Add(new ComplaintConfig());
            modelBuilder.Configurations.Add(new FLKConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new SMOConfig());
        }
    }
}
