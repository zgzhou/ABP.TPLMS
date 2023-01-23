using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.OutStocks.Dto;
using ABP.TPLMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.OutStocks
{
    public class OutStockOrderAppService : AsyncCrudAppService<OutStockOrder, OutStockOrderDto, int, PagedOutStockResultRequestDto,
                            CreateUpdateOutStockOrderDto, CreateUpdateOutStockOrderDto>, IOutStockOrderAppService

    {
        IOutStockOrderRepository rep = null;
        IOutStockOrderDetailAppService isodApp=null;
        
        public OutStockOrderAppService(IRepository<OutStockOrder, int> repository,
            IOutStockOrderRepository isdRepository,IOutStockOrderDetailAppService isodAppSer)
            : base(repository)
        {
            rep = isdRepository;
            isodApp = isodAppSer;
        }
        public  Task<PagedResultDto<OutStockOrderDto>> GetAll(PagedOutStockResultRequestDto input)
        {

            return GetAllAsync(input);
        }
        public override Task<PagedResultDto<OutStockOrderDto>> GetAllAsync(PagedOutStockResultRequestDto input)
        {
                 
            return base.GetAllAsync(input);
        }
        [DontWrapResult]
        public PagedOutStockOrderResultDto<OutStockOrderDto> GetAllOutStockOrders(PagedOutStockResultRequestDto input)
        {
          

            PagedOutStockOrderResultDto<OutStockOrderDto> inSOs = new PagedOutStockOrderResultDto<OutStockOrderDto>();
          
            var allOrgs=GetAll(input);
            inSOs.Rows = allOrgs.Result.Items;
            inSOs.Total = allOrgs.Result.TotalCount;
            return inSOs;
        }
      
        protected override IQueryable<OutStockOrder> CreateFilteredQuery(PagedOutStockResultRequestDto input)
        {
            var qry= base.CreateFilteredQuery(input)
                .Where(t=>t.OwnerName.Contains(input.OwnerName==null?string.Empty:input.OwnerName))
                 .Where(t => t.No.Contains(input.No == null ? string.Empty : input.No))
                .Where(t => t.CreationTime>input.BeginTime)
                .Where(t => t.CreationTime<input.EndTime);
            return qry;
        }
        [DontWrapResult]
        public string GetNo()
        {
            string no = rep.GetNo("ODO");
            return no;
        }
        [DontWrapResult]
        public string ImportInStockDetail(string ids,string No)
        {
            try
            {
               
                //导入货物信息
                rep.ImportInStockOrder(ids, No);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return "OK";
        }

        public string Save(OutStockOrderDto iso)
        {
            try
            {
                CreateUpdateOutStockOrderDto order = ObjectMapper.Map<CreateUpdateOutStockOrderDto>(iso);
                foreach (var item in order.OutStockOrderDetail)
                {
                    CreateUpdateOutStockOrderDetailDto isod = ObjectMapper.Map<CreateUpdateOutStockOrderDetailDto>(item);
                    if (isod.Id > 0)
                    {
                        isodApp.UpdateAsync(isod);
                    }
                    else
                        isodApp.CreateAsync(isod);
                    
                  
                }
               
                order.OutStockOrderDetail = null;
                
                order.Status = 1 ;                
                UpdateAsync(order);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return "OK";
        }

        public bool DeleteById(string Ids)
        {
            var idList = Ids.Split(',');
            bool result = true;
            try
            {

          
            foreach (var id in idList)
            {
                int.TryParse(id, out int intId);
                var iso = GetEntityByIdAsync(intId).GetAwaiter().GetResult();
                PagedOutStockDetailResultRequestDto PagedDetail = new PagedOutStockDetailResultRequestDto
                {
                    InStockNo = iso.No
                };
                var isods = isodApp.GetAllAsync(PagedDetail).GetAwaiter().GetResult();
                
                OutStockOrderDto order = ObjectMapper.Map<OutStockOrderDto>(iso);
                DeleteAsync(order);
            }
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;

        }
    }
}
