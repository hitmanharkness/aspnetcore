using BI.Enterprise.Dto.NGTotalAccessApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BI.ServiceBus.Core.NGTotalAccessApi.Contract
{
	public interface INGTotalAccessApiCore
	{
        // NG Get Menu Items
        List<DtoMenuItem> GetMenuItems(int id);
	}
}