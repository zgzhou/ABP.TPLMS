using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Modules.Dto;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace ABP.TPLMS.Modules
{
    public class ModuleAppService : ApplicationService, IModuleAppService
    {
        private readonly IRepository<Module> _moduleRepository;
      // AutoMapper.IMapper m_map;

        public ModuleAppService(IRepository<Module> moduleRepository)
        {
            _moduleRepository = moduleRepository;
        
           // m_map =map;
        }
        public Task CreateAsync(CreateUpdateModuleDto input)
        {
            var module= ObjectMapper.Map<Module>(input);
            //var module = m_map.Map<Module>(input);

            return _moduleRepository.InsertAsync(module);
        }
        public Task UpdateAsync(CreateUpdateModuleDto input)
        {
            Logger.Info("更新操作-日记记录 - 模块类型的名称 为：" + input.DisplayName);
            var module = ObjectMapper.Map<Module>(input);
           // var module = m_map.Map<Module>(input);

            return _moduleRepository.UpdateAsync(module);
        }
        public async Task<ListResultDto<ModuleDto>> GetAllAsync()
        {
            var books = await _moduleRepository.GetAllListAsync();
            return new ListResultDto<ModuleDto>(ObjectMapper.Map<List<ModuleDto>>(books));
            
        }
        [UnitOfWork(isTransactional:false)]
        public List<Module> GetAll()
        {
            var books = _moduleRepository.GetAllListAsync();
            //   AutoMapper.Mapper.Initialize(map => map.CreateMap<Module, ModuleDto>());
            // List<ModuleDto> slist = AutoMapper.Mapper.Map<List<Module>, List<ModuleDto>>(books);
            //var slist = new ListResultDto<ModuleDto>(ObjectMapper.Map<List<ModuleDto>>(books));
            // return new ListResultDto<ModuleDto>(ObjectMapper.Map<List<ModuleDto>>(books));
            //IEnumerator<Module> enumerator = books.GetEnumerator();
            //// Keep track of whether the row to be deleted
            //// has actually been deleted yet. This allows
            //// this sample to demonstrate that the enumerator
            //// is able to survive row deletion.
            //List<Module> result = new List<Module>();
            //while (enumerator.MoveNext())
            //{
            //    result.Add(enumerator.Current);
            //}

                return books.Result;

        }
        public async Task DeleteAsync(int Id)
        {
             await _moduleRepository.DeleteAsync(Id);
          
        }
        public  void Delete(int Id)
        {
             _moduleRepository.Delete(Id);

        }
    }
}
