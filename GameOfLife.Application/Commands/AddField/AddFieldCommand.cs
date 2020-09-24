using MediatR;
using System;

namespace GameOfLife.Application.Commands
{
    public class AddFieldCommand : IRequest<Guid>
    {
        public AddFieldCommand(UInt16 size)
        {
            Size = size;
        }

        public UInt16 Size { get; }
    }
}
