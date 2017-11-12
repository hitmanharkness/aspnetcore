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
    public class NGTotalAccessApiManager : INGTotalAccessApiManager
    {
        #region Private Variables
        private INGTotalAccessApiCore _core;
        #endregion

        #region Constructor

        public NGTotalAccessApiManager(INGTotalAccessApiCore core)
        {
            this._core = core;
        }

        #endregion

        #region INGTotalAccessApiManager Implementation


        // Recursion, traverse the tree to the leaves looking for the menuitem.
        IMenuItemResponseModel FindMenuItemRecursively(IMenuItemResponseModel parentMenuItem, int menuItemId)
        {
            if (parentMenuItem.MenuId == menuItemId)
                return parentMenuItem; // Found it. 

            // Let me check the submenuItem. 
            if (parentMenuItem.SubMenuItems != null)
            {
                foreach (var childMenuItem in parentMenuItem.SubMenuItems)
                {
                    var parentMenuItemRecurse = FindMenuItemRecursively(childMenuItem, menuItemId); // The parent is submenuItem now and try again.
                    if (parentMenuItemRecurse != null)
                        return parentMenuItemRecurse;
                }
            }

            return null; // We never found anything.
        }

        // We are looping through the root elements that we found immediatly.
        IMenuItemResponseModel FindParentMenuItem(IEnumerable<MenuItemResponseModel> parentMenuItems, int menuItemId)
        {
            foreach (var pitem in parentMenuItems)
            {
                // Find the element in it's tree.
                var proudParent = FindMenuItemRecursively(pitem, menuItemId); // The parent is submenuItem now and try again.

                if (proudParent != null)
                    return proudParent;
            }

            return null; // We never found anything.
        }

        // ///////////////////////////////
        // NG Get Menu Items
        // ///////////////////////////////
        // ????????????????????????????????????????????????????????????????????????????????????????????????????????
        // Ok this generic is for the view model.  The view model may be different than the business model.
        // So that is what the manager does. He changes the presentation layer.  I might call him presentation or presentatino manager.
        public T GetMenuItems<T>(int userId) where T : class, IGetMenuItemsResponseModel, new() // Question: Why would I really want this response to be generic. What exactly does it add?
        {

            // ???????????????????????????????????????????????????????????????
            // FOR THE SAKE OF ARGUMENT LETS JUST SAY I DID WANT ANOTHER MENUITEMRESPONSE TYPE, A DIFFERENT OBJECT TO IMPLEMENT THE GENERIC, SOME EXTRA PROPERTIES PERHAPS, WHAT METHOD WOULD ADD THEM? 
            // IM ALREADY IN THE MANAGER.  
            // LETS THEN SAY WE PASSED IN ANOTHER MODEL THAT ADDED PROPERTIES DONT I STILL HAVE CHANGE THIS CODE SPECIFICALLY TO THE NEW MODEL LIKE COMBINING A FIRST NAME, LAST NAME.
            // I THINK GENERICS ARE USED WHEN YOU WANT THIS FUNCTIONS "FUNCTIONALITY" TO NOT CHANGE, AND THAT IS PROGRAMMING TO AN INTERFACE BUT THIS IS THE TOP LEVEL, WE WOULDNT CHANGE ANYTHING IN THE API LEVEL.
            // SO MY THOUGHT IT GOOD CODING BUT NOT GOOD THINKING BUT LET ME THINK WHERE IT WOULD BE DIFFERENT.
            // ???????????????????????????????????????????????????????????????

            var mName = this.GetCallerMethodName();
            //Log.Verbose("{MethodName} - Manager - Id {Id} - Starting", mName, userId);
            try
            {
                // Order the results for the model because of menu item -> sub menu item.
                List<DtoMenuItem> menuItems = this._core.GetMenuItems(userId).OrderBy(x => x.MenuId).ToList();

                // This will save us some time in the search because we have a real easy way or findig the root parent menuitems.
                List<DtoMenuItem> parentMenuItems = menuItems.Where(y => !y.ParentId.HasValue).ToList();
                List<DtoMenuItem> childMenuItems = menuItems.Where(y => y.ParentId.HasValue).ToList();

                // Testing data.
                // -- 489 Reports
                // -- 501 Activity - parentid 489
                // -- 1252 parentid 501
                //int[] pList = new int[] { 489 };
                //List<DtoMenuItem> parentMenuItems = menuItems.Where(y => !y.ParentId.HasValue && pList.Contains(y.MenuId)).ToList();
                //int[] cList = new int[] { 501, 1252 };
                //List<DtoMenuItem> childMenuItems = menuItems.Where(y => y.ParentId.HasValue && cList.Contains(y.MenuId)).ToList();

                // IM FORCED TO PROGRAM TO THIS T TYPE AND HAVING TO PASS IN GENERIC MODELS BUT FOR WHAT ADVANTAGE YET?
                T menuItemsResponse = new T();
                var _tempMenuItems = new List<MenuItemResponseModel>();
                menuItemsResponse.MenuItems = new List<MenuItemResponseModel>();

                // First we can put all the root parent items into the list because we already easily found them.
                // I'll split these out in two seperate loops to avoid any confusion.
                foreach (var pitem in parentMenuItems)
                    {
                    var parentMenuItem = new MenuItemResponseModel()
                    {
                        MenuId = pitem.MenuId,
                        Title = pitem.Title,
                        Description = pitem.Description,
                        ParentId = pitem.ParentId,
                        MenuRefName = pitem.MenuRefName,
                        MenuUrl = pitem.MenuUrl,
                        HotKey = "hotkey", //item.HotKey,
                        Icon = "icon" //item.Icon
                    };
                    _tempMenuItems.Add(parentMenuItem);
                    //menuItemsResponse.MenuItems.Add(parentMenuItem);
                }
                menuItemsResponse.MenuItems = _tempMenuItems;

                // This enters a recursive tree traverse. The most important part is that the list to traverse is ordered (menuid), else we won't have inserted parents to insert children.
                // Now lets place all the children.
                foreach (var citem in childMenuItems)
                {
                    if (!citem.ParentId.HasValue)
                        continue; // Skip with "what happened?" question.

                    var parentMenuItem = FindParentMenuItem(menuItemsResponse.MenuItems, citem.ParentId.Value);

                    if (parentMenuItem != null)
                    {
                        parentMenuItem.SubMenuItems = parentMenuItem.SubMenuItems ?? new List<IMenuItemResponseModel>();
                        parentMenuItem.SubMenuItems.Add(
                            new MenuItemResponseModel()
                            {
                                MenuId = citem.MenuId,
                                Title = citem.Title,
                                Description = citem.Description,
                                ParentId = citem.ParentId,
                                MenuRefName = citem.MenuRefName,
                                MenuUrl = citem.MenuUrl,
                                HotKey = "hotkey", //item.HotKey,
                                Icon = "icon" //item.Icon
                            });
                    }
                }

                return menuItemsResponse;









                // ???????????????????????????????????
                // The Manager should coordinates any necessary calls to the core.
                //var dto = this._core.GetNGTotalAccessApiInfo(id);
                // TODO: Set the values of the resp variable using the content of the dto variable.
                // ???????????????????????????????????


                // The Manager should coordinates any necessary calls to the core.
                //var dto = this._core.GetMenuItems(id);
                // TODO: Set the values of the resp variable using the content of the dto variable.
                //var resp = new T
                //{
                //    MenuItems = menuItems
                //};
                //return resp;

            }
            catch(Exception ex)
            {
                //Log.Error("Exception in {MethodName} method - Id {Id}", mName, userId);
                throw;
            }
            finally
            {
                //Log.Verbose("{MethodName} - Manager - Id {Id} - Completed", mName, userId);
            }
        }














        //public T GetNGTotalAccessApiInfo<T>(int id) where T : class, INGTotalAccessApiInfo, new()
        //{
        //    var mName = this.GetCallerMethodName();
        //    //Log.Verbose("{MethodName} - Manager - Id {Id} - Starting", mName, id);
        //    try
        //    {
        //        // The Manager should coordinates any necessary calls to the core.
        //        var dto = this._core.GetNGTotalAccessApiInfo(id);
        //        // TODO: Set the values of the resp variable using the content of the dto variable.
        //        var resp = new T
        //        {
        //            ChangeDate = dto.ChangeDate,
        //            CreateDate = dto.CreateDate,
        //            Id = dto.Id,
        //            Name = dto.Name
        //        };
        //        return resp;
        //    }
        //    catch
        //    {
        //        //Log.Error("Exception in {MethodName} method - Id {Id}", mName, id);
        //        throw;
        //    }
        //    finally
        //    {
        //        //Log.Verbose("{MethodName} - Manager - Id {Id} - Completed", mName, id);
        //    }
        //}

        //public int SaveNGTotalAccessApiInfo(INGTotalAccessApiInfo NGTotalAccessApi)
        //{
        //    var mName = this.GetCallerMethodName();
        //    //Log.Verbose("{MethodName} - Manager - Starting", mName);
        //    try
        //    {
        //        // The Manager should coordinates any necessary calls to the core.
        //        var dto = this.CreateDto(NGTotalAccessApi);
        //        // The validation of the DTO should be part of the Core.
        //        // This must be done in the validation rules of the action.
        //        var newId = this._core.SaveNGTotalAccessApiInfo(dto);
        //        return newId;
        //    }
        //    catch
        //    {
        //        Log.Error("Exception in {MethodName} method", mName);
        //        throw;
        //    }
        //    finally
        //    {
        //        Log.Verbose("{MethodName} - Manager - Completed", mName);
        //    }
        //}

        #endregion

        #region Private Methods

        private DtoNGTotalAccessApiInfo CreateDto(INGTotalAccessApiInfo NGTotalAccessApi)
        {
            return new DtoNGTotalAccessApiInfo { Name = NGTotalAccessApi.Name };
        }

        private string GetCallerMethodName([CallerMemberName]string callerName = "") => callerName;

        #endregion



        private string testStringMenuItems = @"[
               {  
                  ""MenuID"":346,
                  ""MenuType"":""header"",
                  ""Title"":""Alerts"",
                  ""Description"":"""",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""NULL"",
                  ""DisplayOrder"":""20000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":348,
                        ""MenuType"":""header"",
                        ""Title"":""All Alert Queue"",
                        ""Description"":"""",
                        ""MenuRefName"":""AllQueue"",
                        ""MenuUrl"":""~/AlertMgmt/AllQueue.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""All Queue"",
                        ""DisplayOrder"":""3000.0"",
                        ""SubMenu"":[

                        ]
                },
                     {  
                        ""MenuID"":356,
                        ""MenuType"":""notheader"",
                        ""Title"":""Alert Processing"",
                        ""Description"":"""",
                        ""MenuRefName"":""AlertProcessing"",
                        ""MenuUrl"":""~/AlertMgmt/AlertProcessing.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Alert Processing"",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
            },
                     {  
                        ""MenuID"":444,
                        ""MenuType"":""header"",
                        ""Title"":""Return Page Alert Queue"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/AlertMgmt/AllQueue.aspx?NewSearch=True&amp;amp;AlertStatus=Delayed&amp;amp;EntitySearchType=AgencyAndOfficer&amp;amp;EntityFilter=Officer"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""All Queue"",
                        ""DisplayOrder"":""4000.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":345,
                  ""MenuType"":""header"",
                  ""Title"":""Agencies"",
                  ""Description"":"""",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""NULL"",
                  ""DisplayOrder"":""30000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":418,
                        ""MenuType"":""header"",
                        ""Title"":""Transfer Inventory "",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Agency/InventoryTransfer.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Inventory Transfer"",
                        ""DisplayOrder"":""11000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":555,
                        ""MenuType"":""header"",
                        ""Title"":""Group Zones"",
                        ""Description"":"""",
                        ""MenuRefName"":""AgencyGroupZones"",
                        ""MenuUrl"":""~/Agency/AgencyGroupZones.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Agency Group Zones"",
                        ""DisplayOrder"":""9000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":650,
                        ""MenuType"":""header"",
                        ""Title"":""Add Agency"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Agency/AddUpdateAgency.aspx?MODE=AGENCY_NEW"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Agency Profile"",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":705,
                        ""MenuType"":""header"",
                        ""Title"":""Agency Settings"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""1500.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":812,
                        ""MenuType"":""header"",
                        ""Title"":""Caseload Snapshot"",
                        ""Description"":"""",
                        ""MenuRefName"":""CaseLoadAdmin"",
                        ""MenuUrl"":""~/Client/CaseloadSnapshot.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Caseload Snapshot"",
                        ""DisplayOrder"":""7000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":813,
                        ""MenuType"":""header"",
                        ""Title"":""All Open Alerts"",
                        ""Description"":"""",
                        ""MenuRefName"":""AllOpenAlerts"",
                        ""MenuUrl"":""~/Agency/AllOpenAlerts.aspx?startPage=AllOpenAlerts"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""All Open Alerts"",
                        ""DisplayOrder"":""8000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":903,
                        ""MenuType"":""header"",
                        ""Title"":""Agency Notification Settings"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""2000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":914,
                        ""MenuType"":""header"",
                        ""Title"":""Pre Defined Notes"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/UniversalNotes/PreDefinedNotesList.aspx?MODE=AGENCY"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""PreDefined Notes"",
                        ""DisplayOrder"":""12000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":951,
                        ""MenuType"":""header"",
                        ""Title"":""Community Providers"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Agency/CommunityProviderManagement.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Community Provider Management"",
                        ""DisplayOrder"":""14000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1013,
                        ""MenuType"":""header"",
                        ""Title"":""Tag Management"",
                        ""Description"":"""",
                        ""MenuRefName"":""AgencyTagMgmt"",
                        ""MenuUrl"":""~/Entity/Tag/TagManagement.aspx?entityTypeId=5"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Agency Tag Management"",
                        ""DisplayOrder"":""13000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1270,
                        ""MenuType"":""header"",
                        ""Title"":""Call Details"",
                        ""MenuRefName"":""VoiceCallDetail"",
                        ""MenuUrl"":""~/User/VoiceCallDetail.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Call Details"",
                        ""DisplayOrder"":""8100.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1297,
                        ""MenuType"":""notheader"",
                        ""Title"":""Dashboard"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Client/CaseloadDashboard.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Dashboard"",
                        ""DisplayOrder"":""9000.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":906,
                  ""MenuType"":""header"",
                  ""Title"":""Users"",
                  ""Description"":"""",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""NULL"",
                  ""DisplayOrder"":""40000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":553,
                        ""MenuType"":""header"",
                        ""Title"":""Verify Officer Pin"",
                        ""Description"":"""",
                        ""MenuRefName"":""VerifyPin"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""5000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":809,
                        ""MenuType"":""header"",
                        ""Title"":""Agency User Settings"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/User/UserList.aspx?MODE=USER"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""User List"",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":814,
                        ""MenuType"":""header"",
                        ""Title"":""Admin User Settings"",
                        ""Description"":"""",
                        ""MenuRefName"":""AdminUserList"",
                        ""MenuUrl"":""~/User/UserList.aspx?MODE=ADMIN_USER"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Admin User List"",
                        ""DisplayOrder"":""3000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":816,
                        ""MenuType"":""notheader"",
                        ""Title"":""Admin User Profile"",
                        ""Description"":"""",
                        ""MenuRefName"":""UpdateAdmin"",
                        ""MenuUrl"":""~/User/AddUpdateUser.aspx?MODE=ADMIN_USER_OTHER"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Admin User Profile"",
                        ""DisplayOrder"":""3100.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":904,
                        ""MenuType"":""header"",
                        ""Title"":""Agency User Notification Settings"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""2000.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":303,
                  ""MenuType"":""header"",
                  ""Title"":""Clients"",
                  ""Description"":"""",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""NULL"",
                  ""DisplayOrder"":""50000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":322,
                        ""MenuType"":""header"",
                        ""Title"":""Device History"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Client/device_history.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Device History"",
                        ""DisplayOrder"":""8000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":410,
                        ""MenuType"":""header"",
                        ""Title"":""Audit Trail"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Reports/AuditTrail/Client.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Audit Trail"",
                        ""DisplayOrder"":""7000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":413,
                        ""MenuType"":""notheader"",
                        ""Title"":""Audit Trail"",
                        ""Description"":"""",
                        ""MenuRefName"":""ATGroup"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""7000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":414,
                        ""MenuType"":""notheader"",
                        ""Title"":""Client Activity Reports"",
                        ""Description"":"""",
                        ""MenuRefName"":""RptGroup"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""2000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":538,
                        ""MenuType"":""header"",
                        ""Title"":""Add Client"",
                        ""Description"":"""",
                        ""MenuRefName"":""AddNewClient"",
                        ""MenuUrl"":""~/Client/AddUpdateClient.aspx?PAGEMODE=CLIENT_NEW"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Client Profile"",
                        ""DisplayOrder"":""2000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":881,
                        ""MenuType"":""header"",
                        ""Title"":""Alerts and Settings"",
                        ""Description"":"""",
                        ""MenuRefName"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""4000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":900,
                        ""MenuType"":""header"",
                        ""Title"":""Administration"",
                        ""Description"":"""",
                        ""MenuRefName"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""6000.0"",
                        ""SubMenu"":[
                            {  
                                ""MenuID"":538,
                                ""MenuType"":""header"",
                                ""Title"":""Add Client"",
                                ""Description"":"""",
                                ""MenuRefName"":""AddNewClient"",
                                ""MenuUrl"":""~/Client/AddUpdateClient.aspx?PAGEMODE=CLIENT_NEW"",
                                ""IsReadOnly"":0,
                                ""PageTitle"":""Client Profile"",
                                ""DisplayOrder"":""2000.0"",
                                ""SubMenu"":[

                                ]
                            }
                        ]
                     },
                     {  
                        ""MenuID"":901,
                        ""MenuType"":""header"",
                        ""Title"":""Search"",
                        ""Description"":"""",
                        ""MenuRefName"":""Client Search"",
                        ""MenuUrl"":""~/Client/ClientSearch.aspx?sMode=blank"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Search"",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":902,
                        ""MenuType"":""header"",
                        ""Title"":""Notification Settings"",
                        ""Description"":"""",
                        ""MenuRefName"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""5000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":930,
                        ""MenuType"":""header"",
                        ""Title"":""Current Client"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Client/AddUpdateClient.aspx?"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Client Profile"",
                        ""DisplayOrder"":""3000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1014,
                        ""MenuType"":""header"",
                        ""Title"":""Tag Management"",
                        ""Description"":"""",
                        ""MenuRefName"":""ClientTagMgmt"",
                        ""MenuUrl"":""~/Entity/Tag/TagManagement.aspx?entityTypeId=4"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Client Tag Management"",
                        ""DisplayOrder"":""11000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1200,
                        ""MenuType"":""header"",
                        ""Title"":""Case Notes"",
                        ""Description"":"""",
                        ""MenuRefName"":""CaseNotes"",
                        ""MenuUrl"":""~/UniversalNotes/CaseNotes.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Case Notes"",
                        ""DisplayOrder"":""6100.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1201,
                        ""MenuType"":""hidden"",
                        ""Title"":""Profile"",
                        ""Description"":"""",
                        ""MenuRefName"":""ClientProfile"",
                        ""MenuUrl"":""~/Client/Profile.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""New Client Profile"",
                        ""DisplayOrder"":""20000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1202,
                        ""MenuType"":""hidden"",
                        ""Title"":""Enroll Client"",
                        ""Description"":"""",
                        ""MenuRefName"":""EnrollClient"",
                        ""MenuUrl"":""~/Client/Enroll.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Enroll Client"",
                        ""DisplayOrder"":""21000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1295,
                        ""MenuType"":""notheader"",
                        ""Title"":""Notification Suspension"",
                        ""Description"":"""",
                        ""MenuRefName"":""RptNotificationSuspension"",
                        ""MenuUrl"":""~/Reports/HtmlReports/NotificationSuspension.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Audit Trail"",
                        ""DisplayOrder"":""8000000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1369,
                        ""MenuType"":""header"",
                        ""Title"":""Community Referrals"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Client/CommunityReferrals.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Community Referrals"",
                        ""DisplayOrder"":""8010.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1451,
                        ""MenuType"":""header"",
                        ""Title"":""Documents"",
                        ""Description"":"""",
                        ""MenuRefName"":""ClientDocuments"",
                        ""MenuUrl"":""~/Client/ClientDocuments.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Documents"",
                        ""DisplayOrder"":""12000.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":907,
                  ""MenuType"":""header"",
                  ""Title"":""Maps"",
                  ""Description"":"""",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""NULL"",
                  ""DisplayOrder"":""60000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":525,
                        ""MenuType"":""header"",
                        ""Title"":""Point In Time"",
                        ""Description"":"""",
                        ""MenuRefName"":""Point In Time"",
                        ""MenuUrl"":""~/Reports/Mapping/PointInTime.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Point In Time"",
                        ""DisplayOrder"":""5000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":527,
                        ""MenuType"":""header"",
                        ""Title"":""Address Lookup"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Lib/AddressLookup.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Address Lookup"",
                        ""DisplayOrder"":""6000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":541,
                        ""MenuType"":""header"",
                        ""Title"":""Aerial Mapping Summary"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Reports/Mapping/AerialMappingSummary.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Aerial Mapping Summary"",
                        ""DisplayOrder"":""3000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":908,
                        ""MenuType"":""header"",
                        ""Title"":""Enhanced Mapping"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Mapping/ClientMappingControl.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Mapping"",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":1400,
                  ""MenuType"":""header"",
                  ""Title"":""Analytics"",
                  ""Description"":"""",
                  ""MenuRefName"":""Analytics"",
                  ""MenuUrl"":""~/Reports/ReportLauncher.aspx"",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""Report Launcher"",
                  ""DisplayOrder"":""65000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":1311,
                        ""MenuType"":""header"",
                        ""Title"":""Tamper Alerts Analysis"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""reftamperalerts"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=272"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2300.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1401,
                        ""MenuType"":""header"",
                        ""Title"":""Priority Analytics"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":"""",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1402,
                        ""MenuType"":""header"",
                        ""Title"":""Proximity Analytics"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":"""",
                        ""DisplayOrder"":""2000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1453,
                        ""MenuType"":""header"",
                        ""Title"":""Performance Analysis"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":"""",
                        ""DisplayOrder"":""2100.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":489,
                  ""MenuType"":""header"",
                  ""Title"":""Reports"",
                  ""Description"":"""",
                  ""MenuRefName"":""Report Launcher"",
                  ""MenuUrl"":""~/Reports/ReportLauncher.aspx"",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""Report Launcher"",
                  ""DisplayOrder"":""70000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":501,
                        ""MenuType"":""header"",
                        ""Title"":""Activity"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""1.1"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":510,
                        ""MenuType"":""header"",
                        ""Title"":""Administrative"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""1.3"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":515,
                        ""MenuType"":""header"",
                        ""Title"":""ISAP III - Field Reports"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.8"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":518,
                        ""MenuType"":""header"",
                        ""Title"":""ISAP III - Customer Reports"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.7"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":874,
                        ""MenuType"":""header"",
                        ""Title"":""Client Information"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""1.5"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1241,
                        ""MenuType"":""header"",
                        ""Title"":""ISAP III - Billing Reports"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.6"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1344,
                        ""MenuType"":""header"",
                        ""Title"":""FCMP Services Received"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""FCMSServicesReceived"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=299"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.3"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1359,
                        ""MenuType"":""header"",
                        ""Title"":""North Carolina DPS"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.9"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1362,
                        ""MenuType"":""header"",
                        ""Title"":""FCMP Intake"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""FCMPIntake"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=311"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""1.9"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1363,
                        ""MenuType"":""header"",
                        ""Title"":""FCMP Compliance"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""FCMPCompliance"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=290"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""1.7"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1374,
                        ""MenuType"":""header"",
                        ""Title"":""FCMP Supervised HoH Counts"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""FCMPActiveHoHCounts"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=317"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.4"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1375,
                        ""MenuType"":""header"",
                        ""Title"":""FCMP Supervised HoH Details"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""FCMPActiveHoHDetails"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=318"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.5"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1395,
                        ""MenuType"":""header"",
                        ""Title"":""FL DJJ"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""3.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1413,
                        ""MenuType"":""header"",
                        ""Title"":""FCMP Participant Detail"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""FCMPParticipantDetai"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=338"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""2.1"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1414,
                        ""MenuType"":""header"",
                        ""Title"":""FCMP Inactive Participants"",
                        ""Description"":""Crystal Report"",
                        ""MenuRefName"":""FCMPInactiveParticip"",
                        ""MenuUrl"":""~/Reports/BIServiceBased/BIServiceBasedReportRunner.aspx?reportID=339"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Crystal Report"",
                        ""DisplayOrder"":""1.8"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":445,
                  ""MenuType"":""header"",
                  ""Title"":""System"",
                  ""Description"":"""",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""NULL"",
                  ""DisplayOrder"":""80000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":446,
                        ""MenuType"":""header"",
                        ""Title"":""Alert Lock Management"",
                        ""Description"":"""",
                        ""MenuRefName"":""AlertLockManagement"",
                        ""MenuUrl"":""~/Admin/AlertLockManagement.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Alert Lock Management"",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":750,
                        ""MenuType"":""header"",
                        ""Title"":""User Access Groups"",
                        ""Description"":"""",
                        ""MenuRefName"":""AccessGroups"",
                        ""MenuUrl"":""~/Admin/AccessGroups.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Access Groups"",
                        ""DisplayOrder"":""4000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":751,
                        ""MenuType"":""hidden"",
                        ""Title"":""Add Edit Access Groups"",
                        ""Description"":"""",
                        ""MenuRefName"":""AddEditAccessGroups"",
                        ""MenuUrl"":""~/Admin/AddEditAccessGroups.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Add Edit Access Groups"",
                        ""DisplayOrder"":""4100.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":752,
                        ""MenuType"":""hidden"",
                        ""Title"":""Add Edit Members"",
                        ""Description"":"""",
                        ""MenuRefName"":""AddEditMembers"",
                        ""MenuUrl"":""~/Admin/AddEditMembers.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Add Edit Members"",
                        ""DisplayOrder"":""4200.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":818,
                        ""MenuType"":""header"",
                        ""Title"":""Inventory Management"",
                        ""Description"":"""",
                        ""MenuUrl"":""~/Agency/InventoryManage.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Manage Inventory"",
                        ""DisplayOrder"":""7000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1452,
                        ""MenuType"":""header"",
                        ""Title"":""VoiceId Call Rescheduling"",
                        ""Description"":""VoiceId Call Rescheduling"",
                        ""MenuRefName"":""VoiceIdCallRescheduling"",
                        ""MenuUrl"":""~/Admin/VoiceIdCallRescheduling.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Voice Call Rescheduling"",
                        ""DisplayOrder"":""10000.0"",
                        ""SubMenu"":[

                        ]
                     },
                     {  
                        ""MenuID"":1460,
                        ""MenuType"":""header"",
                        ""Title"":""Customer"",
                        ""Description"":"""",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""NULL"",
                        ""DisplayOrder"":""6000.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               },
               {  
                  ""MenuID"":868,
                  ""MenuType"":""hidden"",
                  ""Title"":""News"",
                  ""Description"":"""",
                  ""MenuRefName"":""News"",
                  ""MenuUrl"":""~/user/CustomerCommunicationList.aspx"",
                  ""IsReadOnly"":0,
                  ""PageTitle"":""News"",
                  ""DisplayOrder"":""90000.0"",
                  ""SubMenu"":[
                     {  
                        ""MenuID"":923,
                        ""MenuType"":""notheader"",
                        ""Title"":""Message"",
                        ""Description"":"""",
                        ""MenuRefName"":""Message"",
                        ""MenuUrl"":""~/user/CustomerCommunication.aspx"",
                        ""IsReadOnly"":0,
                        ""PageTitle"":""Message"",
                        ""DisplayOrder"":""1000.0"",
                        ""SubMenu"":[

                        ]
                     }
                  ]
               }
            ]".Replace("\r\n", string.Empty).Replace(System.Environment.NewLine, string.Empty).Replace("\"", "\"");


    }
}