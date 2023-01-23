using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ABP.TPLMS.Controllers
{
    public abstract class TPLMSControllerBase: AbpController
    {
        protected TPLMSControllerBase()
        {
            LocalizationSourceName = TPLMSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        protected dynamic JsonEasyUI(dynamic t,int total)
        {          
            var obj= new
            {
                total = total,
                rows = t
            };
            var json = ABP.TPLMS.Helpers.JsonHelper.Instance.Serialize(obj);
          //  var  json = Json(obj);
            return json;
        }
        protected dynamic JsonEasyUIResult(int id,string result)
        {
            string strId = string.Empty;
            if (id>0)
            {
                strId = id.ToString();
            }
            var obj = new
            {
                result = result,
                Id = strId
            };
            var json = ABP.TPLMS.Helpers.JsonHelper.Instance.Serialize(obj);
            //  var  json = Json(obj);
            return json;
        }
        protected string JsonEasyUI(object obj)
        {   
            var json = ABP.TPLMS.Helpers.JsonHelper.Instance.Serialize(obj);            
            return json;
        }
    }
}
