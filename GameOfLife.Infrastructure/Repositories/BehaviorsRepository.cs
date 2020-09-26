using GameOfLife.Domain.Behaviors;
using GameOfLife.Domain.Enumerations;
using GameOfLife.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Infrastructure.Repositories
{
    public class BehaviorsRepository : IBehaviorsRepository
    {
        IDictionary<BehaviorEnum, IBehavior> _store = new Dictionary<BehaviorEnum, IBehavior>
        {
            [BehaviorEnum.Null] = new NullBehavior(),
            [BehaviorEnum.Conway] = new ConwayBehavior()
        };

        public async Task<IBehavior> Get(BehaviorEnum id)
        {
            if (_store.TryGetValue(id, out IBehavior result))
            {
                return await Task.FromResult(result);
            }

            return await Task.FromResult(new NullBehavior());
        }

        public async Task<IBehavior[]> ToArray()
        {
            return await Task.FromResult(
                _store.Values.ToArray()
            );
        }
    }
}
