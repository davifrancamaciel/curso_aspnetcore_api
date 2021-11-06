using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;

namespace Api.Service.Services
{
    public class ZipCodeService : IZipCodeService
    {
        public Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<ZipCodeDTO> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ZipCodeDTO> Get(string zipCode)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ZipCodeDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ZipCodeCreateResultDTO> Post(ZipCodeCreateDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ZipCodeUpdateResultDTO> Put(ZipCodeUpdateDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
