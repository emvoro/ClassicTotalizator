using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.SportDTOs;
using Microsoft.EntityFrameworkCore;
using ClassicTotalizator.DAL.Entities;

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

        public async Task<SportDTO> Add(SportDTO sport)
        {
            if (sport == null)
                throw new ArgumentNullException(nameof(sport));

            var sportEntity = SportMapper.Map(sport);
            try
            {
                await _context.Sports.AddAsync(sportEntity);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return null;
            }
            return sport;
        }

        public async Task<SportsDTO> GetCurrentListOfSports()
        {
            var sports = await _context.Sports.ToListAsync() ?? new List<Sport>();

            return new SportsDTO
            {
                Sports = sports.Select(SportMapper.Map).ToList()
            };
        }
    }
}
