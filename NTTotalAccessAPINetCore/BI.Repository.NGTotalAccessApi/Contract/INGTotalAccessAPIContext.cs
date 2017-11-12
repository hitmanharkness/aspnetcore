using BI.Repository.ApplicationSetting.Models;
using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

namespace BI.Repository.NGTotalAccessApi.Contract
{
	public interface INGTotalAccessApiContext
	{
        #region Table Collection

        //DbSet<AppLanguage> AppLanguage { get; set; }
        //DbSet<Menu> Menu { get; set; } // Menu Table

        // TA procedure for getting menu items.
        //ObjectResult<MenuItem> AppUserMenuAccessGet(int userId);
        IEnumerable<MenuItem> AppUserMenuAccessGet(int userId);

        // TODO: Reference to table collections. Check Officer mobile for examples.
        #endregion

        #region Methods

        //int SaveChangesInDb();

		#endregion
	}
}