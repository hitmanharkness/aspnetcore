//using BI.Enterprise.Common.Exceptions;
//using BI.Enterprise.Common.Translation.Enums;
using BI.Enterprise.Dto.NGTotalAccessApi;
using BI.Enterprise.ViewModels.NGTotalAccessApi;
using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;
using BI.ServiceBus.Core.NGTotalAccessApi.Contract;
using BI.ServiceBus.Manager.NGTotalAccessApi.Contract;
//using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BI.ServiceBus.Manager.NGTotalAccessApi
{
    public class NGTotalAccessApiManagerStub : INGTotalAccessApiManager
    {
        #region Private Variables
        //private INGTotalAccessApiCore _core;
        #endregion

        #region Constructor

        //public NGTotalAccessApiManager(INGTotalAccessApiCore core)
        //{
        //    this._core = core;
        //}

        #endregion

        #region INGTotalAccessApiManager Implementation
        T INGTotalAccessApiManager.GetMenuItems<T>(int id)
        {
            var stubData_MenuItems = new List<MenuItemResponseModel>();
            stubData_MenuItems.Add(new MenuItemResponseModel()
            {
                MenuId = 1,
                Title = "Alerts",
                ParentId = null,
                Description = "",
                MenuRefName = "",
                MenuUrl = "http://menuurl.com",
                HotKey = "hotkey",
                Icon = ""
            });
            stubData_MenuItems.Add(new MenuItemResponseModel()
            {
                MenuId = 2,
                Title = "Reports",
                ParentId = null,
                Description = "",
                MenuRefName = "",
                MenuUrl = "http://menuurl.com",
                HotKey = "hotkey",
                Icon = ""
            });
            return new T() {
                MenuItems = stubData_MenuItems
            };
        }

        #endregion

        #region Private Methods



        #endregion

    }
}

//            {
//                ""MenuID"":346,
//                  ""MenuType"":""header"",
//                  ""Title"":""Alerts"",
//                  ""Description"":"""",
//                  ""IsReadOnly"":0,
//                  ""PageTitle"":""NULL"",
//                  ""DisplayOrder"":""20000.0"",
//                  ""SubMenu"":[
//                     {  
//                        ""MenuID"":348,
//                        ""MenuType"":""header"",
//                        ""Title"":""All Alert Queue"",
//                        ""Description"":"""",
//                        ""MenuRefName"":""AllQueue"",
//                        ""MenuUrl"":""~/AlertMgmt/AllQueue.aspx"",
//                        ""IsReadOnly"":0,
//                        ""PageTitle"":""All Queue"",
//                        ""DisplayOrder"":""3000.0"",
//                        ""SubMenu"":[

//                        ]
//    },
//                     {  
//                        ""MenuID"":356,
//                        ""MenuType"":""notheader"",
//                        ""Title"":""Alert Processing"",
//                        ""Description"":"""",
//                        ""MenuRefName"":""AlertProcessing"",
//                        ""MenuUrl"":""~/AlertMgmt/AlertProcessing.aspx"",
//                        ""IsReadOnly"":0,
//                        ""PageTitle"":""Alert Processing"",
//                        ""DisplayOrder"":""1000.0"",
//                        ""SubMenu"":[

//                        ]
//},