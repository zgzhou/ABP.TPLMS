using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.InStocks.Dto;
using ABP.TPLMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.InStocks
{
    public class InStockOrderAppService : AsyncCrudAppService<InStockOrder, InStockOrderDto, int, PagedInStockResultRequestDto,
                            CreateUpdateInStockOrderDto, CreateUpdateInStockOrderDto>, IInStockOrderAppService

    {
        IInStockOrderRepository rep = null;
        IInStockOrderDetailAppService isodApp=null;
        IInStockOrderDetailLocAppService isodLocApp = null;
        public InStockOrderAppService(IRepository<InStockOrder, int> repository,
            IInStockOrderRepository isdRepository,IInStockOrderDetailAppService isodAppSer
            ,IInStockOrderDetailLocAppService isodLocAppSer)
            : base(repository)
        {
            rep = isdRepository;
            isodApp = isodAppSer;
            isodLocApp = isodLocAppSer;
        }
        public  Task<PagedResultDto<InStockOrderDto>> GetAll(PagedInStockResultRequestDto input)
        {

            return GetAllAsync(input);
        }
        public override Task<PagedResultDto<InStockOrderDto>> GetAllAsync(PagedInStockResultRequestDto input)
        {
                 
            return base.GetAllAsync(input);
        }
        [DontWrapResult]
        public PagedInStockOrderResultDto<InStockOrderDto> GetAllInStockOrders(PagedInStockResultRequestDto input)
        {
          

            PagedInStockOrderResultDto<InStockOrderDto> inSOs = new PagedInStockOrderResultDto<InStockOrderDto>();
          
            var allOrgs=GetAll(input);
            inSOs.Rows = allOrgs.Result.Items;
            inSOs.Total = allOrgs.Result.TotalCount;
            return inSOs;
        }
      
        protected override IQueryable<InStockOrder> CreateFilteredQuery(PagedInStockResultRequestDto input)
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
            string no = rep.GetNo("GDE");
            return no;
        }
        [DontWrapResult]
        public string ImportCargo(string ids,string No)
        {
            try
            {
               
                //导入货物信息
                rep.ImportCargo(ids, No);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return "OK";
        }

        public string Save(InStockOrderDto iso)
        {
            try
            {
                CreateUpdateInStockOrderDto order = ObjectMapper.Map<CreateUpdateInStockOrderDto>(iso);
                foreach (var item in order.InStockOrderDetail)
                {
                    CreateUpdateInStockOrderDetailDto isod = ObjectMapper.Map<CreateUpdateInStockOrderDetailDto>(item);
                    if (isod.Id > 0)
                    {
                        isodApp.UpdateAsync(isod);
                    }
                    else
                        isodApp.CreateAsync(isod);
                    
                  
                }
                foreach (var loc in iso.InStockOrderDetailLoc)
                {
                    CreateUpdateInStockOrderDetailLocDto isodLoc = ObjectMapper.Map<CreateUpdateInStockOrderDetailLocDto>(loc);
                    if (isodLoc.Id > 0)
                    {
                        isodLocApp.UpdateAsync(isodLoc);
                    }
                    else
                        isodLocApp.CreateAsync(isodLoc);

                }
                order.InStockOrderDetail = null;
                order.InStockOrderDetail = null;
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
                PagedInStockDetailResultRequestDto PagedDetail = new PagedInStockDetailResultRequestDto
                {
                    InStockNo = iso.No
                };
                var isods = isodApp.GetAllAsync(PagedDetail).GetAwaiter().GetResult();
                foreach (var dod in isods.Items)
                {
                    PagedInStockDetailLocResultRequestDto PagedLoc = new PagedInStockDetailLocResultRequestDto
                    {
                        InStockOrderDetailId = dod.Id
                    };
                    var isodLocs = isodLocApp.GetAllAsync(PagedLoc).GetAwaiter().GetResult();
                    foreach (var loc in isodLocs.Items)
                    {
                        isodLocApp.DeleteAsync(loc);
                    }
                    isodApp.DeleteAsync(dod);
                }
                InStockOrderDto order = ObjectMapper.Map<InStockOrderDto>(iso);
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

        public dynamic LoadInodLocs(string rcv, string cargoName)
        {
            List<InStockOrderDetail> list = new List<InStockOrderDetail>();
            //入库单明细表

            var dods = rep.GetInodLocs(rcv, cargoName);
            foreach (var item in dods)
            {
                list.Add(item);
            }
            return new
            {
                total = list.Count,
                rows = list
            };
        }

    }
}
