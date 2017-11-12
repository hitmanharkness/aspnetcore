using BI.Enterprise.Dto.NGTotalAccessApi;
using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;
using System;
using System.Collections.Generic;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi
{
	public class GetMenuItemsResponseModel : IGetMenuItemsResponseModel
    {
        public IEnumerable<MenuItemResponseModel> MenuItems { get; set; }

    }
}