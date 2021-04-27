using AutoMapper;
using GameOfLife.Application.Dto;
using GameOfLife.Domain.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;

namespace GameOfLife.Application.Mappings
{
	public class FieldProfile : Profile
	{
		public FieldProfile()
		{
			CreateMap<Field, FieldDto>()
				.ForMember(d => d.Name, o => o.MapFrom(s => $"Generation #{s.Generation}"))
				.ForMember(d => d.Data, o => o.MapFrom(s => ValuesToData(s.Values, s.Size)));

			CreateMap<FieldDto, Field>();
		}

		private Byte[] ValuesToData(Byte[] values, UInt16 size)
		{
			Byte[] data;

			using (MemoryStream output = new MemoryStream())
			using (Image<Rgba32> image = new Image<Rgba32>(size, size, new Rgba32(0, 0, 0, 0)))
			{
				for (Int32 j = 0; j < size; j++)
				{
					Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(j);
					for (Int32 i = 0; i < size; i++)
					{
						Byte value = values[j * size + i];

						if (value == 1)
						{
							pixelRowSpan[i] = new Rgba32(0, 0, 0, 255);
						}
					}
				}

				image.SaveAsPng(output);

				data = output.ToArray();
			}

			return data;
		}
	}
}
