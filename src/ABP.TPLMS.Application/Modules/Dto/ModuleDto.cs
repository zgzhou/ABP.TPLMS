﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ABP.TPLMS.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.Modules.Dto
{
    [AutoMapFrom(typeof(Module))]
    public class ModuleDto:EntityDto<int>
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }       
        public string Url { get; set; }        
        public string HotKey { get; set; }
        public int ParentId { get; set; }
        public bool RequiresAuthentication { get; set; }
        public bool IsAutoExpand { get; set; }        
        public string IconName { get; set; }
        public int Status { get; set; }
        public string ParentName { get; set; }       
        public string RequiredPermissionName { get; set; }
        public int SortNo { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
