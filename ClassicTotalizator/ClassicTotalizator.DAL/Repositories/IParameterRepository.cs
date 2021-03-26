using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        Task<IEnumerable<Parameter>> GetParametersByParticipantId(Guid partId);
    }
}