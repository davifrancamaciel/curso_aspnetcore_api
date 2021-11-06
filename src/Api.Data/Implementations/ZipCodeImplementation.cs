using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class ZipCodeImplementation : BaseRepository<ZipCodeEntity>, IZipCodeRepository
    {
        private DbSet<ZipCodeEntity> _dataset;

        public ZipCodeImplementation(MyContext context) :
            base(context)
        {
            _dataset = _context.Set<ZipCodeEntity>();
        }

        public async Task<ZipCodeEntity> SelectAsync(string zipcode)
        {
            return await _dataset.Include(c => c.City).ThenInclude(s => s.State).FirstOrDefaultAsync(z => z.ZipCode.Equals(zipcode));
        }
    }
}
