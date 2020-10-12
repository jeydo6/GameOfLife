using GameOfLife.Domain.Enumerations;
using MediatR;
using System;

namespace GameOfLife.Application.Commands
{
    public class AddFieldCommand : IRequest<Guid>
    {
        public AddFieldCommand(UInt16 size, Byte density, BehaviorEnum behaviorEnum)
        {
            Size = size;
            Density = density;
            BehaviorEnum = behaviorEnum;
        }

        public UInt16 Size { get; }

        public Byte Density { get; }

        public BehaviorEnum BehaviorEnum { get; }
    }
}
