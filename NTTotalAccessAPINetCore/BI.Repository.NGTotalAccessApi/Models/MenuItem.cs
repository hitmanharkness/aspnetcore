namespace BI.Repository.ApplicationSetting.Models
{
	public class MenuItem
    {
		//public virtual App App { get; set; } // What was this?
		public int MenuId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public int? ParentId { get; set; }
        public string MenuRefName { get; set; }
		public string MenuUrl { get; set; }
		//public string HotKey { get; set; }
		//public string Icon { get; set; }

        //      MenuID,
        //MenuType,
        //Title,
        //Description,
        //MenuAction,
        //MenuRefName,
        //MenuUrl,
        //IsReadOnly,
        //ReportDisplayOrder,
        //ReportOpenerID,
        //PageTitle,
        //MenuStyle,
        //HotKey,
        //ControlKey,
        //ClientClickAction,
        //displayDetails,
        //IsReport,
        //ParentID,
        //DisplayOrder,
        //TabGroup,
        //FunctionGroupId,
        //      StartDate,
        //      EndDate,
        //      HasBeenDemo  

    }
}