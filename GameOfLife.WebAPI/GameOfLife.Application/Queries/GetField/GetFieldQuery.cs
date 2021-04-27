using GameOfLife.Application.Dto;
using MediatR;
using System;

namespace GameOfLife.Application.Queries
{
	public class GetFieldQuery : IRequest<FieldDto>
	{
		public GetFieldQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; }
	}
}
