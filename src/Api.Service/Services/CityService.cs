using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.City;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
    public class CityService : ICityService
    {

        private ICityRepository _repository;
        private readonly IMapper _mapper;
        public CityService(ICityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public async Task<CityDTO> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<CityDTO>(entity);
        }

        public async Task<IEnumerable<CityDTO>> GetAll()
        {
            var entity = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<CityDTO>>(entity);
        }

        public async Task<CityCompleteDTO> GetFullByIBGE(int codeIBGE)
        {
            var entity = await _repository.GetCompleteByIBGE(codeIBGE);
            return _mapper.Map<CityCompleteDTO>(entity);
        }

        public async Task<CityCompleteDTO> GetFullById(Guid id)
        {
            var entity = await _repository.GetCompleteById(id);
            return _mapper.Map<CityCompleteDTO>(entity);
        }

        public async Task<CityCreateResultDTO> Post(CityCreateDTO city)
        {
            var model = _mapper.Map<CityModel>(city);
            var entity = _mapper.Map<CityEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<CityCreateResultDTO>(result);
        }

        public async Task<CityUpdateResultDTO> Put(CityUpdateDTO city)
        {
            var model = _mapper.Map<CityModel>(city);
            var entity = _mapper.Map<CityEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<CityUpdateResultDTO>(result);
        }
    }
}
