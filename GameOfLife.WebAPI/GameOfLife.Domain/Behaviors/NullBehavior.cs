using System;

namespace GameOfLife.Domain.Behaviors
{
	public class NullBehavior : IBehavior
	{
		public Byte[] Generate(UInt16 size, Byte density, Int32 seed)
		{
			return new Byte[size * size];
		}

		public Byte[] Iterate(Byte[] values)
		{
			return values;
		}
	}
}
