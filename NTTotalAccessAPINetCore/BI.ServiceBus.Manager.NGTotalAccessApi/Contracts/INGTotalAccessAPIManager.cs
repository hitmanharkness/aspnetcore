using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;
using System.Threading.Tasks;

namespace BI.ServiceBus.Manager.NGTotalAccessApi.Contract
{
	public interface INGTotalAccessApiManager
	{
        // ///////////////////////////////
        // NG Get Menu Items
        // ///////////////////////////////


        // ?????????????????? THIS SHOULD BE HAVE THE RESPONSE NAME OR NOT? IGetMenuItemsResponse
        T GetMenuItems<T>(int id)
            where T : class, IGetMenuItemsResponseModel, new();



        //T GetNGTotalAccessApiInfo<T>(int id)
		//	where T : class, INGTotalAccessApiInfo, new();

		//int SaveNGTotalAccessApiInfo(INGTotalAccessApiInfo NGTotalAccessApi);
	}
}