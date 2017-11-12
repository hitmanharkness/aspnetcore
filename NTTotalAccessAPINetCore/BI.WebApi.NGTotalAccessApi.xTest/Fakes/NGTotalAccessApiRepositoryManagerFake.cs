using BI.Enterprise.Dto.NGTotalAccessApi;
using BI.Repository.NGTotalAccessApi.Contract;
using BI.ServiceBus.Repository.NGTotalAccessApi.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BI.ServiceBus.Repository.NGTotalAccessApi
{
	public class NGTotalAccessApiRepositoryManagerFake : INGTotalAccessApiRepositoryManager
	{
		#region Private Variables
		#endregion

		#region Constructor

		public NGTotalAccessApiRepositoryManagerFake()
        { 
		}

        #endregion

        #region INGTotalAccessApiRepositoryManager Implementation

        // Menu Items Testing
        public IEnumerable<DtoMenuItem> GetMenuItems(int id)
        {
            var menuItems = new List<DtoMenuItem>();        

            var menuItem1 = new DtoMenuItem() {
                MenuId = 1,
                Title = "",
                Description = "",
                ParentId = 2,
                MenuRefName = "",
                MenuUrl = "http://whereever"
            };
            menuItems.Add(menuItem1);

            return menuItems;
        }

		public int Save()
		{
			return 1; // why am i returning 1?
		}


        #endregion
    }
}