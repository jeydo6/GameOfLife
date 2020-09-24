using AutoMapper;
using GameOfLife.Application.Dto;
using GameOfLife.Domain.Entities;

namespace GameOfLife.Application.Mappings
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<Field, FieldDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => $"Generation #{s.Generation}"));

            CreateMap<FieldDto, Field>();
        }
    }
}
