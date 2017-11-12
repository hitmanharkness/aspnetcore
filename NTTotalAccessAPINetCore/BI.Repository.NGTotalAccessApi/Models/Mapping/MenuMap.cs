//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration;

//namespace BI.Repository.ApplicationSetting.Models.Mapping
//{
//	public class MenuMap : EntityTypeConfiguration<Menu>
//	{
//		public MenuMap()
//		{
//			// Primary Key
//			this.HasKey(t => t.MenuId);

//			// Properties
//			this.Property(t => t.MenuId)
//				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//			this.Property(t => t.MenuId)
//				.IsRequired();

//            // ??????????????????????
//			//this.Property(t => t.Parameter)
//			//	.IsRequired();

//			// Table & Column Mappings
//			this.ToTable("Menu");
//			this.Property(t => t.MenuId).HasColumnName("MenuId");
//			this.Property(t => t.Title).HasColumnName("Title");
//			//this.Property(t => t.LanguageId).HasColumnName("LanguageId");

//			//this.HasRequired(t => t.App)
//			//	.WithMany(t => t.AppLanguages)
//			//	.HasForeignKey(d => d.AppId);
//		}
//	}
//}