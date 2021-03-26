using ClassicTotalizator.BLL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.SportDTOs;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    /// <summary>
    /// Adds sports
    /// </summary>
    public class SportService : ISportService
    {
        private readonly ISportRepository _repository;

        public SportService(ISportRepository repository)
        {
            _repository = repository;
        }

        public async Task<SportDTO> Add(SportDTO sport)
        {
            if (sport == null) throw new ArgumentNullException(nameof(sport));

            var sportEntity = SportMapper.Map(sport);

            try
            {
                await _repository.AddAsync(sportEntity);
            }
            catch (Exception)
            {
                return null;
            }

            return sport;
        }

        public async Task<SportsDTO> GetCurrentListOfSports()
        {
            var sports = await _repository.GetAllAsync() ?? new List<Sport>();

            return new SportsDTO
            {
                Sports = sports.Select(SportMapper.Map).ToList()
            };
        }

        public async Task<bool> DeleteSportAsync(int id)
        {
            var sport = await _repository.GetByIdAsync(id);
            if (sport == null) 
                return false;

            await _repository.RemoveAsync(sport);

            return true;
        }
    }
}
