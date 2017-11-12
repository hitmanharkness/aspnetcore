using BI.Enterprise.Dto.NGTotalAccessApi;
using System;
using System.Collections.Generic;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi.Contract
{
	public interface IGetMenuItemsResponseModel
	{
        IEnumerable<MenuItemResponseModel> MenuItems { get; set; }

    }
}