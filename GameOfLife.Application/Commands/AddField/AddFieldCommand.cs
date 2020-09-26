using GameOfLife.Domain.Enumerations;
using MediatR;
using System;

namespace GameOfLife.Application.Commands
{
    public class AddFieldCommand : IRequest<Guid>
    {
        public AddFieldCommand(UInt16 size, Byte density, BehaviorEnum behavior)
        {
            Size = size;
            Density = density;
            Behavior = behavior;
        }

        public UInt16 Size { get; }

        public Byte Density { get; }

        public BehaviorEnum Behavior { get; }
    }
}
