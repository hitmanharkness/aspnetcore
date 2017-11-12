using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;
using System;
using System.Collections.Generic;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi
{
	public class MenuItemResponseModel : IMenuItemResponseModel
    {
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string MenuRefName { get; set; }
        public string MenuUrl { get; set; }
        public string HotKey { get; set; }
        public string Icon { get; set; }

        public List<IMenuItemResponseModel> SubMenuItems { get; set; }
        
    }
}