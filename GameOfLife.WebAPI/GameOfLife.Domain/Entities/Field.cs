using GameOfLife.Domain.Behaviors;
using System;

namespace GameOfLife.Domain.Entities
{
	public class Field
	{
		public Field(UInt16 size, Byte density, IBehavior behavior)
		{
			Id = Guid.NewGuid();
			DateTime = DateTime.Now;
			Size = size;
			Behavior = behavior;

			Values = Behavior.Generate(Size, density, 0);
		}

		public Guid Id { get; }

		public DateTime DateTime { get; private set; }

		public UInt32 Generation { get; private set; }

		public Byte[] Values { get; private set; }

		public UInt16 Size { get; }

		private IBehavior Behavior { get; }

		public Field Next()
		{
			Values = Behavior.Iterate(Values);
			DateTime = DateTime.Now;
			Generation++;

			return this;
		}
	}
}
