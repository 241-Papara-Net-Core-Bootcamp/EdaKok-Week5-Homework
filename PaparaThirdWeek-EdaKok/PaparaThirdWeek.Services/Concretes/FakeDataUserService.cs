using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PaparaThirdWeek.Services.DTOS;

namespace PaparaThirdWeek.Services.Concretes
{
  public  class FakeDataUserService:IFakeDataUserService
    {
        private readonly IGenericRepository<FakeDataUser> repository;
        private readonly ICacheService cacheService;
        private const string cacheKey = "UserCacheKey"; //Unique key
        private readonly IMapper mapper;

        public FakeDataUserService(IGenericRepository<FakeDataUser> _repository, ICacheService _cacheService, IMapper _mapper)
        {
            repository = _repository;
            cacheService = _cacheService;
            mapper = _mapper;
        }
        public void Add(FakeDataUserDTO userDTO)
        {
            var user = mapper.Map<FakeDataUser>(userDTO);
            var cachedList = repository.Add(user);
            //refresh cache 
            cacheService.Remove(cacheKey);
            cacheService.Set(cacheKey, cachedList);
        }

        public List<FakeDataUser> GetAllUsers()
        {
            var userList = repository.GetAll().ToList();
            cacheService.Set(cacheKey, userList);                  
            cacheService.TryGet<FakeDataUser>(cacheKey, out userList);         
            return userList;
        }
    }
}
