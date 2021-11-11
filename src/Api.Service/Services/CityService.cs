using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;

namespace Api.Service.Services
{
    public class CityService : ICityService
    {
        public Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<CityDTO> Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CityDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CityFullDTO> GetFullByIBGE(int codeIBGE)
        {
            throw new NotImplementedException();
        }

        public Task<CityFullDTO> GetFullById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CityCreateResultDTO> Post(CityCreateDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<CityUpdateResultDTO> Put(CityUpdateDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
