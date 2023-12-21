using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegionService(IRegionRepository regionRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<RegionDTO>> GetRegionId(string state)
        {
            var regionObj = await _regionRepository.GetRegionId(state);

            if (regionObj == null)
                return ResultService.Fail<RegionDTO>("Error null obj city");

            return ResultService.Ok(_mapper.Map<RegionDTO>(regionObj));
        }

        public async Task<ResultService<RegionDTO>> GetIdByNameCity(string state)
        {
            var regionObj = await _regionRepository.GetIdByNameCity(state);

            if (regionObj == null)
                return ResultService.Fail<RegionDTO>("Error null obj city");

            return ResultService.Ok(_mapper.Map<RegionDTO>(regionObj));
        }
    }
}
