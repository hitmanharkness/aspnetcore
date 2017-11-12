using System;
using System.Collections.Generic;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi.Contract
{
	public interface IMenuItemViewModel
	{
        int MenuId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string ParentId { get; set; }
        string MenuRefName { get; set; }
        string MenuUrl { get; set; }
        string HotKey { get; set; }
        string Icon { get; set; }

        IMenuItemViewModel SubMenuItem { get; set; }
    }
}