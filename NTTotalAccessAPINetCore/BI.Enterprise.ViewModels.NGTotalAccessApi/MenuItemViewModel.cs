using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;
using System;
using System.Collections.Generic;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi
{
	public class MenuItemViewModel : IMenuItemViewModel
    {
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }
        public string MenuRefName { get; set; }
        public string MenuUrl { get; set; }
        public string HotKey { get; set; }
        public string Icon { get; set; }

        public IMenuItemViewModel SubMenuItem { get; set; }
    }
}