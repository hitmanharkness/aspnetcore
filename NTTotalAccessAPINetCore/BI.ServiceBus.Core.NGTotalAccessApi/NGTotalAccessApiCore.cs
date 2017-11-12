//using BI.Enterprise.Common.Exceptions;
using BI.Enterprise.Dto.NGTotalAccessApi;
//using BI.ServiceBus.Core.NGTotalAccessApi.Action;
using BI.ServiceBus.Core.NGTotalAccessApi.Contract;
using BI.ServiceBus.Repository.NGTotalAccessApi.Contract;
//using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BI.ServiceBus.Core.NGTotalAccessApi
{
	public class NGTotalAccessApiCore : INGTotalAccessApiCore
	{
		#region Private Variables
		private INGTotalAccessApiRepositoryManager _repository;
		#endregion

		#region Constructor

		public NGTotalAccessApiCore(INGTotalAccessApiRepositoryManager repository)
		{
			this._repository = repository;
		}

        #endregion

        #region INGTotalAccessApiCore Implementation

        //public List<DtoMenuItem> GetMenuItems(int id)
        public List<DtoMenuItem> GetMenuItems(int id)
        {
            var mName = this.GetCallerMethodName();
            //Log.Verbose("{MethodName} - Core - Id {Id} - Starting", mName, id);
            try
            {
                // TODO: Instantiate all the required actions and execute each one individually. Check the response of each action to
                // decide if the next action should be executed. If the process needs to be terminated due to an invalid action, your should
                // throw a validation exception with the information related to the error. Is easier for unit testing if you return the first
                // error instead of collecting all the errors and return all of them at once. Also is easier for the exception handlers of the API
                // to manage one error at a time specially if a translation is required for the response.

                /// INJECTION HERE or the repository????????????????????????????????????????????????????????????? 
                //var action = new GetMenuItemsAction(this._repository, id); 

                //action.Execute();
                //if (action.WorkValidationContext.IsValid)
                //    return action.Response;
                //var error = action.WorkValidationContext.FailedResults.First();
                //throw new ValidationException(error.ErrorCode, error.Message);


            }
            catch (Exception ex)
            {
                //Log.Error("Exception in {MethodName} method - Core - Id {Id}", mName, id);
                throw;
            }
            finally
            {
                //Log.Verbose("{MethodName} - Core - Id {Id} - Completed", mName, id);
            }

            return null; //????????????????????/
        }



        //public DtoNGTotalAccessApiInfo GetNGTotalAccessApiInfo(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public int SaveNGTotalAccessApiInfo(DtoNGTotalAccessApiInfo dto)
        //{
        //    throw new NotImplementedException();
        //}









        //      public DtoNGTotalAccessApiInfo GetNGTotalAccessApiInfo(int id)
        //{
        //	var mName = this.GetCallerMethodName();
        //	Log.Verbose("{MethodName} - Core - Id {Id} - Starting", mName, id);
        //	try
        //	{
        //		// TODO: Instantiate all the required actions and execute each one individually. Check the response of each action to
        //		// decide if the next action should be executed. If the process needs to be terminated due to an invalid action, your should
        //		// throw a validation exception with the information related to the error. Is easier for unit testing if you return the first
        //		// error instead of collecting all the errors and return all of them at once. Also is easier for the exception handlers of the API
        //		// to manage one error at a time specially if a translation is required for the response.
        //		var action = new GetNGTotalAccessApiInfoAction(this._repository, id);
        //		action.Execute();
        //		if (action.WorkValidationContext.IsValid)
        //			return action.Response;
        //		var error = action.WorkValidationContext.FailedResults.First();
        //		throw new ValidationException(error.ErrorCode, error.Message);
        //	}
        //	catch
        //	{
        //		//Log.Error("Exception in {MethodName} method - Core - Id {Id}", mName, id);
        //		throw;
        //	}
        //	finally
        //	{
        //		//Log.Verbose("{MethodName} - Core - Id {Id} - Completed", mName, id);
        //	}
        //}

        //public int SaveNGTotalAccessApiInfo(DtoNGTotalAccessApiInfo dto)
        //{
        //	var mName = this.GetCallerMethodName();
        //	Log.Verbose("{MethodName} - Core - Starting", mName);
        //	try
        //	{
        //		// TODO: Instantiate all the required actions and execute each one individually. Check the response of each action to
        //		// decide if the next action should be executed. If the process needs to be terminated due to an invalid action, your should
        //		// throw a ValidationException with the information related to the error. Is easier for unit testing if you return the first
        //		// error instead of collecting all the errors and return all of them at once. Also is easier for the exception handlers of the API
        //		// to manage one error at a time specially if a translation is required for the response.
        //		var action = new SaveNGTotalAccessApiInfoAction(this._repository, dto);
        //		action.Execute();
        //		if (action.WorkValidationContext.IsValid)
        //			return action.Response;
        //		var error = action.WorkValidationContext.FailedResults.First();
        //		throw new ValidationException(error.ErrorCode, error.Message);
        //	}
        //	catch
        //	{
        //		//Log.Error("Exception in {MethodName} method - Core", mName);
        //		throw;
        //	}
        //	finally
        //	{
        //		Log.Verbose("{MethodName} - Core - Completed", mName);
        //	}
        //}

        #endregion

        #region Private Methods

        private string GetCallerMethodName([CallerMemberName]string callerName = "") => callerName;

		#endregion
	}
}