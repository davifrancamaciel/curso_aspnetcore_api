using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CityImplementation : BaseRepository<CityEntity>, ICityRepository
    {
        private DbSet<CityEntity> _dataset;

        public CityImplementation(MyContext context) :
            base(context)
        {
            _dataset = _context.Set<CityEntity>();
        }

        public async Task<CityEntity> GetCompleteByIBGE(int codeIBGE)
        {
            return await _dataset.Include(s => s.State).FirstOrDefaultAsync(x => x.IbgeId == codeIBGE);
        }

        public async Task<CityEntity> GetCompleteById(Guid id)
        {
            return await _dataset.Include(s => s.State).FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
