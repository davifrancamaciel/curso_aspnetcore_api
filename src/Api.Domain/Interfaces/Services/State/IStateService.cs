using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.State;

namespace Api.Domain.Interfaces.Services.State
{
    public interface IStateService
    {
        Task<StateDTO> Get(Guid Id);

        Task<IEnumerable<StateDTO>> GetAll();
    }
}
