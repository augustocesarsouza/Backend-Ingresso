using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IUserPermissionRepository _repository;
        private readonly IMapper _mapper;

        public UserPermissionService(IUserPermissionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultService<List<UserPermissionDTO>>> GetAllPermissionUser(Guid? idUser)
        {
            var permission = await _repository.GetAllPermissionUser(idUser);
            if (permission == null || permission.Count <= 0)
                return ResultService.Fail<List<UserPermissionDTO>>("not fould");

            return ResultService.Ok(_mapper.Map<List<UserPermissionDTO>>(permission));
        }
    }
}
