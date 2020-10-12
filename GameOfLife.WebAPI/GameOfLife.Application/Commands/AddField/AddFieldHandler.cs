using GameOfLife.Domain.Behaviors;
using GameOfLife.Domain.Entities;
using GameOfLife.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Commands
{
    public class AddFieldHandler : IRequestHandler<AddFieldCommand, Guid>
    {
        private readonly IFieldsRepository _fields;
        private readonly IBehaviorsRepository _behaviors;

        public AddFieldHandler(
            IFieldsRepository fields,
            IBehaviorsRepository behaviors
        )
        {
            _fields = fields;
            _behaviors = behaviors;
        }

        public async Task<Guid> Handle(AddFieldCommand request, CancellationToken cancellationToken)
        {
            IBehavior behavior = await _behaviors.Get(request.BehaviorEnum);

            return await _fields.Add(
                new Field(request.Size, request.Density, behavior)
            );
        }
    }
}
