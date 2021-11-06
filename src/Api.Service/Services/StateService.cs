using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.State;
using Api.Domain.Interfaces.Services.State;

namespace Api.Service.Services
{
    public class StateService : IStateService
    {
        public Task<StateDTO> Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StateDTO>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
