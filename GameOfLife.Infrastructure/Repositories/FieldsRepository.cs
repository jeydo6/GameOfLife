using GameOfLife.Domain.Entities;
using GameOfLife.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Infrastructure.Repositories
{
    public class FieldsRepository : IFieldsRepository
    {
        private readonly IDictionary<Guid, Field> _store = new Dictionary<Guid, Field>();

        public async Task<Guid> Add(Field item)
        {
            if (_store.ContainsKey(item.Id))
            {
                _store[item.Id] = item;
            }
            else
            {
                _store.Add(item.Id, item);
            }

            return await Task.FromResult(item.Id);
        }

        public async Task<Field> Get(Guid id)
        {
            await Clear();

            if (_store.TryGetValue(id, out Field result))
            {
                return await Task.FromResult(result);
            }

            return await Task.FromResult<Field>(null);
        }

        public async Task Remove(Guid id)
        {
            if (_store.ContainsKey(id))
            {
                _store.Remove(id);
            }

            await Task.CompletedTask;
        }

        public Task<Field[]> ToArray()
        {
            return Task.FromResult(
                _store.Values.ToArray()
            );
        }

        private async Task Clear()
        {
            ICollection<Guid> keys = new List<Guid>();

            DateTime dateTime = DateTime.Now.AddMinutes(-5);
            foreach (var item in _store)
            {
                if (item.Value.DateTime < dateTime)
                {
                    keys.Add(item.Key);
                }
            }

            foreach(var key in keys)
            {
                await Remove(key);
            }

            await Task.CompletedTask;
        }
    }
}
