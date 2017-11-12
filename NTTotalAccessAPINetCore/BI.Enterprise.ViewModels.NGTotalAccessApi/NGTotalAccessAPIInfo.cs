namespace BI.Enterprise.ViewModels.NGTotalAccessApi
{
	using Contract;
	using System;

	public class NGTotalAccessApiInfo : INGTotalAccessApiInfo
	{
		// TODO: Create and Change date values should be set by the system when the data is being saved in the database.
		#region INGTotalAccessApiInfo Explicit Implementation
		DateTime INGTotalAccessApiInfo.ChangeDate { get; set; }
		DateTime INGTotalAccessApiInfo.CreateDate { get; set; }
		#endregion

		public int Id { get; set; }
		public string Name { get; set; }
	}
}