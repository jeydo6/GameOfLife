using GameOfLife.Application.Dto;
using MediatR;
using System;

namespace GameOfLife.Application.Queries
{
    public class NextFieldQuery : IRequest<FieldDto>
    {
        public NextFieldQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
