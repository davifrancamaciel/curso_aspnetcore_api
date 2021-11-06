using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.ZipCode;

namespace Api.Domain.Interfaces.Services.ZipCode
{
    public interface IZipCodeService
    {
        Task<ZipCodeDTO> Get(Guid id);

        Task<ZipCodeDTO> Get(string zipCode);

        Task<IEnumerable<ZipCodeDTO>> GetAll();

        Task<ZipCodeCreateResultDTO> Post(ZipCodeCreateDTO model);

        Task<ZipCodeUpdateResultDTO> Put(ZipCodeUpdateDTO model);

        Task<bool> Delete(Guid Id);
    }
}
