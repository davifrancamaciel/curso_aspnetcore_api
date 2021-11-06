using System;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface ICityRepository : IRepository<CityEntity>
    {
        Task<CityEntity> GetFullById(Guid id);

        Task<CityEntity> GetFullByIBGE(int codeIBGE);
    }
}
