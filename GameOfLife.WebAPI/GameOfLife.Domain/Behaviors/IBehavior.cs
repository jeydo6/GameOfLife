using System;

namespace GameOfLife.Domain.Behaviors
{
	public interface IBehavior
	{
		Byte[] Generate(UInt16 size, Byte density, Int32 seed);

		Byte[] Iterate(Byte[] values);
	}
}
