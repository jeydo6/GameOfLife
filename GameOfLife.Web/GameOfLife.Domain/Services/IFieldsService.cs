using GameOfLife.Domain.Dto;
using GameOfLife.Domain.Enumerations;
using System;
using System.Threading.Tasks;

namespace GameOfLife.Domain.Services
{
    public interface IFieldsService
    {
        Task<Guid> Add(UInt16 size, Byte density, BehaviorEnum behaviorEnum);

        Task<FieldDto> Get(Guid id);

        Task Next(Guid id);

        Task Delete(Guid id);
    }
}
