using MediatR;
using System;

namespace GameOfLife.Application.Commands
{
    public class AddFieldCommand : IRequest<Guid>
    {
        public AddFieldCommand(UInt16 size, Byte density)
        {
            Size = size;
            Density = density;
        }

        public UInt16 Size { get; }

        public Byte Density { get; }
    }
}
