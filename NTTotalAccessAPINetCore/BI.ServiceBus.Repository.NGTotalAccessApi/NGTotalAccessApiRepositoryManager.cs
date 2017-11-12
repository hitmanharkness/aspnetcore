using BI.Enterprise.Dto.NGTotalAccessApi;
using BI.Repository.NGTotalAccessApi.Contract;
using BI.ServiceBus.Repository.NGTotalAccessApi.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BI.ServiceBus.Repository.NGTotalAccessApi
{

    /// <summary>
    /// / ????????????????????????????????????????????????????????????????? Iquerable ? What happened to that? I think we missed out on ef cooliest feature.
    /// </summary>
	public class NGTotalAccessApiRepositoryManager : INGTotalAccessApiRepositoryManager
	{
		#region Private Variables
		private readonly INGTotalAccessApiContext _context;
		#endregion

		#region Constructor

		public NGTotalAccessApiRepositoryManager(INGTotalAccessApiContext context)
		{
			this._context = context ?? throw new ArgumentNullException(nameof(context));
		}

        #endregion

        #region INGTotalAccessApiRepositoryManager Implementation

        // Menu Items Testing
        //public List<DtoMenuItem> GetMenuItems(int id)
        //{
        //    var menuItems = new List<DtoMenuItem>();
        //    var menuItem1 = new DtoMenuItem() { Name = "Menu Item 1", url = "http://whereever" };
        //    var menuItem2 = new DtoMenuItem() { Name = "Menu Item 2", url = "http://whereever" };
        //    menuItems.Add(menuItem1);
        //    menuItems.Add(menuItem2);

        //    return menuItems;
        //}



        //public async Task<List<DtoMenuItem>> GetMenuItems(int id)
        public IEnumerable<DtoMenuItem> GetMenuItems(int userId)
        {

            //var menu = this._context.Menu.FirstOrDefault(x => x.MenuId == 315);
            //var menus = new List<DtoMenuItem>();
            //var menuItem1 = new DtoMenuItem() { MenuId = menu.MenuId, Title = menu.Title };
            //menus.Add(menuItem);

            var menuItems = this._context.AppUserMenuAccessGet(userId).ToList();

            return menuItems.Select(x => new DtoMenuItem
            {
                MenuId = x.MenuId,
                Title = x.Title,
                Description = x.Description,
                ParentId = x.ParentId,
                MenuRefName = x.MenuRefName,
                MenuUrl = x.MenuUrl
                //HotKey = x.HotKey,
                //Icon = x.Icon,
            }).ToList();


            //var requestLanguage = this._context.AppLanguage.FirstOrDefault(la => la.LanguageTag.Equals(cultureCode));
            //    var requestLanguageId = requestLanguage?.LanguageId ?? 1;

            //    var query =
            //        from al in db.AppLanguages
            //        from la in db.Languages
            //        from rt in db.ResourceTranslations
            //        where la.LanguageId == al.AppLanguageId
            //        where rt.ResourceId == la.LanguageNameResource
            //        where al.AppId == (long)app
            //        where rt.LanguageId == requestLanguageId
            //        select new
            //        {
            //            la.LanguageId,
            //            la.LanguageTag,
            //            LanguageCode = al.LanguageId,
            //            LanguageName = rt.ResourceString
            //        };

            //    return query.Select(x => new DtoLanguage
            //    {
            //        LanguageId = x.LanguageId,
            //        CultureCode = x.LanguageTag,
            //        CultureName = x.LanguageName,
            //        LanguageCodeLegacyId = x.LanguageCode
            //    }).ToList();
            //}
            //});
        }





  //      public DtoNGTotalAccessApiInfo GetNGTotalAccessApiInfo(int id)
		//{
		//	// TODO: Implement query using _context.
		//	return new DtoNGTotalAccessApiInfo
		//	{
		//		ChangeDate = DateTime.UtcNow,
		//		CreateDate = DateTime.UtcNow,
		//		Id = id,
		//		Name = "Test Name"
		//	};
		//}

		public int Save()
		{
			// TODO: Implement logic to save data into database.
			//this._context.SaveChangesInDb();
			return 1;
		}

		#endregion
	}
}