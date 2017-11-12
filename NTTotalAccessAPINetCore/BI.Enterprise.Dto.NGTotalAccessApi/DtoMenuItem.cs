using System;
using System.Collections.Generic;

namespace BI.Enterprise.Dto.NGTotalAccessApi
{
	public class DtoMenuItem
	{
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string MenuRefName { get; set; }
        public string MenuUrl { get; set; }
        //public string HotKey { get; set; }
        //public string Icon { get; set; }

    }
}