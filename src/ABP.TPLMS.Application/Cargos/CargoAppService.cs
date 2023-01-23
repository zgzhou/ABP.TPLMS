using Abp.Application.Services;
using Abp.Domain.Repositories;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Cargos.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;

namespace ABP.TPLMS.Cargos
{
   public class CargoAppService :AsyncCrudAppService<Cargo, CargoDto, int, PagedCargoResultRequestDto,
                            CreateUpdateCargoDto, CreateUpdateCargoDto>,ICargoAppService
        
    {
        public CargoAppService(IRepository<Cargo, int> repository)
            : base(repository)
    {
        
    }
        public string DeleteBatch(string ids)
        {
            string result = "NO";
            var idList = ids.Split(',');
            foreach (var item in idList)
            {
                var id = 0;
                int.TryParse(item,out id);
                var cargoList = base.GetEntityByIdAsync(id);
                var cargo=MapToEntityDto(cargoList.GetAwaiter().GetResult());
                base.DeleteAsync(cargo);
                result = "OK";
            }
            return result;
        }
        protected override IQueryable<Cargo> CreateFilteredQuery(PagedCargoResultRequestDto input)
        {
            return base.CreateFilteredQuery(input)
                 .Where(t => t.CargoName.Contains(input.CargoName))
                 .Where(t => t.CargoCode.Contains(input.CargoCode))
                 .Where(t => t.HSCode.Contains(input.HsCode))
                 ;

        }
    }
}
