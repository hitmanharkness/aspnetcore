using System;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi.Contract
{
	public interface INGTotalAccessApiInfo
	{
		DateTime ChangeDate { get; set; }
		DateTime CreateDate { get; set; }
		int Id { get; set; }
		string Name { get; set; }
	}
}