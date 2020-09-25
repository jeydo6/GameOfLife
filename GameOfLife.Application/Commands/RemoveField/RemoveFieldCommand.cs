using MediatR;
using System;

namespace GameOfLife.Application.Commands
{
    public class RemoveFieldCommand : IRequest
    {
        public RemoveFieldCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
