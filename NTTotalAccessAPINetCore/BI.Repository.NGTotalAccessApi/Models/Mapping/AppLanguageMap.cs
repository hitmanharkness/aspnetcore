//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration;

//namespace BI.Repository.ApplicationSetting.Models.Mapping
//{
//	public class AppLanguageMap : EntityTypeConfiguration<AppLanguage>
//	{
//		public AppLanguageMap()
//		{
//			// Primary Key
//			this.HasKey(t => t.AppLanguageId);

//			// Properties
//			this.Property(t => t.AppLanguageId)
//				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//			this.Property(t => t.AppId)
//				.IsRequired();

//			this.Property(t => t.LanguageId)
//				.IsRequired();

//			// Table & Column Mappings
//			this.ToTable("AppLanguage");
//			this.Property(t => t.AppLanguageId).HasColumnName("AppLanguageId");
//			this.Property(t => t.AppId).HasColumnName("AppId");
//			this.Property(t => t.LanguageId).HasColumnName("LanguageId");

//			this.HasRequired(t => t.App)
//				.WithMany(t => t.AppLanguages)
//				.HasForeignKey(d => d.AppId);
//		}
//	}
//}