using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;
using System;

namespace BI.Enterprise.ViewModels.NGTotalAccessApi
{
	public class GetNGTotalAccessApiResponseInfo : INGTotalAccessApiInfo
	{
		public DateTime ChangeDate { get; set; }
		public DateTime CreateDate { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
	}
}