using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using DAL.Repositories;
using Ninject.Infrastructure.Language;
using Presentation.DAL.EF;

namespace BLL.Services
{
    public class SomeInfoSRVS
    {
        public static SomeInfoDTO ReturnInfoDtos()
        {
            using (var uow = new UnitOfWork())
            {
                // Mapper.Initialize(cfg => cfg.CreateMap<SomeInfo, SomeInfoDTO>());

                // var oneObj = uow.GetRepository<SomeInfo>().FirstOrDefault();
                // var oneDTOObj = Mapper.Map<SomeInfoDTO>(oneObj);

                //// List<SomeInfoDTO> r = uow.GetRepository<SomeInfo>().All().Select(n=>Mapper.Map<SomeInfoDTO>(n) as SomeInfoDTO).ToList();

                // return oneDTOObj;

                Mapper.Initialize(cfg => cfg.CreateMap<SomeInfo, SomeInfoDTO>());

                var oneObj = uow.GetRepository<SomeInfo>().All().ToList();
                var oneDTOObj = Mapper.Map<List<SomeInfo>,List<SomeInfoDTO>>(oneObj);

                // List<SomeInfoDTO> r = uow.GetRepository<SomeInfo>().All().Select(n=>Mapper.Map<SomeInfoDTO>(n) as SomeInfoDTO).ToList();

                return oneDTOObj[0];



            }
        }
    }
}