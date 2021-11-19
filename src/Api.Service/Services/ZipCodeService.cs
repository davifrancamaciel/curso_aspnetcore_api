using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.ZipCode;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
    public class ZipCodeService : IZipCodeService
    {
        private IZipCodeRepository _repository;
        private readonly IMapper _mapper;

        public ZipCodeService(IZipCodeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public async Task<ZipCodeDTO> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<ZipCodeDTO>(entity);
        }

        public async Task<ZipCodeDTO> Get(string zipCode)
        {
            var entity = await _repository.SelectAsync(zipCode);
            return _mapper.Map<ZipCodeDTO>(entity);
        }

        public async Task<IEnumerable<ZipCodeDTO>> GetAll()
        {
            var entity = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<ZipCodeDTO>>(entity);
        }

        public async Task<ZipCodeCreateResultDTO> Post(ZipCodeCreateDTO zipCode)
        {
            var model = _mapper.Map<ZipCodeModel>(zipCode);
            var entity = _mapper.Map<ZipCodeEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<ZipCodeCreateResultDTO>(result);
        }

        public async Task<ZipCodeUpdateResultDTO> Put(ZipCodeUpdateDTO zipCode)
        {
            var model = _mapper.Map<ZipCodeModel>(zipCode);
            var entity = _mapper.Map<ZipCodeEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<ZipCodeUpdateResultDTO>(result);
        }
    }
}
