using Abp.Application.Navigation;
using Abp.Localization;
using ABP.TPLMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABP.TPLMS.Web.Startup
{
    public class DynamicAddMenu
    {
         Modules.IModuleAppService _moduleAppService;
        public DynamicAddMenu(Modules.IModuleAppService moduleApp)
        { _moduleAppService = moduleApp; }
        public  MenuItemDefinition AddMenus()
        {
            #region 动态菜单
           var modules= _moduleAppService.GetAllList();
            var project = new MenuItemDefinition(
                    "Business",
                    L("Business"),                    
                    icon: "menu",
                    //requiredPermissionName: PermissionNames.Pages_Administration_Projects,
                    order: 5
                    );

            //这里模拟从数据库加载数据
            //for (int i = 1; i <= 10; i++)
            //{
            //    project.AddItem(new MenuItemDefinition(
            //           "p1",
            //           L("项目" + i),
            //           url: "project",
            //           icon: "menu-icon fa fa-tasks",
            //           requiredPermissionName: PermissionNames.Pages_Administration_Projects,
            //           customData: i
            //       ));
            //}
            var list = modules.ToList();
            FillMenu(project, 0, list);
            return project;
            #endregion


        }

        // 递归算法
        private  void FillMenu(MenuItemDefinition menu, int ParentId, List<Module> modules)
        {
            List<Module> drs = modules.Where(x=>x.ParentId==ParentId).ToList();
            if (drs == null || drs.Count <=0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < drs.Count; i++)
                {
                    Module dr = drs[i];
                    MenuItemDefinition nodeName = new MenuItemDefinition(
                       dr.Name,
                       L(dr.DisplayName),
                       url: dr.Url,
                       icon: "business",
                     //  requiredPermissionName: dr.RequiredPermissionName,
                       customData: i
                   );
                    menu.AddItem(nodeName);
                    FillMenu(nodeName, dr.Id, modules);
                }
            }
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TPLMSConsts.LocalizationSourceName);
        }
    }
}
