using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    /// <summary>
    /// Adds sports
    /// </summary>
    public class SportService : ISportService
    {
        private readonly DatabaseContext _context;

        public SportService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(SportDTO sport)
        {
            if (sport == null)
                throw new ArgumentNullException(nameof(sport));

            var sportEntity = SportMapper.Map(sport);

            await _context.Sports.AddAsync(sportEntity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
