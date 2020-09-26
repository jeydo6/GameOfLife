using MediatR;
using System;

namespace GameOfLife.Application.Commands
{
    public class NextFieldCommand : IRequest
    {
        public NextFieldCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
