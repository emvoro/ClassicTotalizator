using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class ParameterRepository : Repository<Parameter>, IParameterRepository
    {
        public ParameterRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Parameter>> GetParametersByParticipantIdAsync(Guid partId)
        {
            return await Set.Where(x => x.Participant_Id == partId).ToListAsync();
        }
    }
}