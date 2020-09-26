using MediatR;
using System;

namespace GameOfLife.Application.Queries
{
    public class RemoveFieldQuery : IRequest
    {
        public RemoveFieldQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
