using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;

namespace Api.Domain.Interfaces.Services.City
{
    public interface ICityService
    {
        Task<CityDTO> Get(Guid Id);

        Task<CityCompleteDTO> GetCompleteById(Guid id);

        Task<CityCompleteDTO> GetCompleteByIBGE(int codeIBGE);

        Task<IEnumerable<CityDTO>> GetAll();

        Task<CityCreateResultDTO> Post(CityCreateDTO model);

        Task<CityUpdateResultDTO> Put(CityUpdateDTO model);

        Task<bool> Delete(Guid Id);
    }
}
