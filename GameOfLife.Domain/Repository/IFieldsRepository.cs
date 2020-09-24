using GameOfLife.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GameOfLife.Domain.Repository
{
    public interface IFieldsRepository
    {
        Task<Guid> Add(Field item);

        Task<Field> Get(Guid id);

        Task Remove(Guid id);

        Task<Field[]> ToArray();
    }
}
