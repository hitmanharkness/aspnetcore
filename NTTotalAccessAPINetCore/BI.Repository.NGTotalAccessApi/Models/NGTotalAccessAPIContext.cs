//using System.Data.Entity;

namespace BI.Repository.NGTotalAccessApi.Models
{
    using BI.Repository.ApplicationSetting.Models;
    using Contract;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    //using System.Data.Entity.Core.Objects;
    //using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    public class NGTotalAccessApiContext : DbContext, INGTotalAccessApiContext
	{
		#region Constructor

		public NGTotalAccessApiContext() //: base("Name=BICaseMgmtContext")
		{ }

        //public ObjectResult<MenuItem> AppUserMenuAccessGet(int userId)
        //{
        //    throw new System.NotImplementedException();
        //}

        public IEnumerable<MenuItem> AppUserMenuAccessGet(int userId)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Table Representations
        // TODO: Implement the required database models. Check Officer Mobile for examples.



        // This if wen I want change to a link query.
        //public DbSet<Menu> Menu { get; set; } // This will be plural if not for protected override void OnModelCreating(DbModelBuilder modelBuilder) below.


        //public virtual ObjectResult<MenuItem> AppUserMenuAccessGet(int userId)
        //{
        //    var officerIdParameter = new SqlParameter("@UserID", userId);

        //    var result = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<MenuItem>("AppUserMenuAccessGet @UserID", officerIdParameter);

        //    return result;
        //}


        #endregion

        #region INGTotalAccessApiContext Implementation
        // TODO: Implement INGTotalAccessApiContext interface. Check Officer Mobile for examples.
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //Database.SetInitializer<NGTotalAccessApiContext>(null);
        //    //base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Menu>().ToTable("Menu");
        //}

        //public int SaveChangesInDb()
		//{

		//	return base.SaveChanges();
		//}

		#endregion
	}

	#region Support class to import the assembly EntityFramework.SqlServer into the bin folder

	//internal class ImportSqlClient
	//{
	//	public System.Data.Entity.SqlServer.SqlProviderServices Test = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
	//}

	#endregion
}