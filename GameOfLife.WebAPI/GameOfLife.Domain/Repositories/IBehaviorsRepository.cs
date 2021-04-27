using GameOfLife.Domain.Behaviors;
using GameOfLife.Domain.Enumerations;
using System.Threading.Tasks;

namespace GameOfLife.Domain.Repositories
{
	public interface IBehaviorsRepository
	{
		Task<IBehavior> Get(BehaviorEnum id);

		Task<IBehavior[]> ToArray();
	}
}
