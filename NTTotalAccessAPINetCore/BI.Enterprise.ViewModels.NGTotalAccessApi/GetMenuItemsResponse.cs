
using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;
using System.Collections.Generic;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi
{
	public class GetMenuItemsResponse : IGetMenuItemsResponse
    {
        public List<MenuItemViewModel> MenuItems { get; set; }

    }
}