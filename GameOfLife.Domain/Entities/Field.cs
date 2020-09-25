using System;

namespace GameOfLife.Domain.Entities
{
    public class Field
    {
        private readonly Action<Field> _lifeAction;

        public Field(UInt16 size, Byte density, Action<Field> lifeAction)
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.Now;
            Size = (UInt16)(size + 2);
            Values = Generate(density, 0);

            _lifeAction = lifeAction;
        }

        public Guid Id { get; }

        public DateTime DateTime { get; private set; }

        public UInt32 Generation { get; private set; }

        public Byte[] Values { get; }

        public UInt16 Size { get; }

        public Field Next()
        {
            _lifeAction?.Invoke(this);

            Generation++;
            DateTime = DateTime.Now;

            return this;
        }

        private Byte[] Generate(Byte density, Int32 seed)
        {
            Byte[] values = new Byte[Size * Size];

            Random random = new Random(seed);
            for (UInt16 i = 1; i < Size - 1; i++)
            {
                for (UInt16 j = 1; j < Size - 1; j++)
                {
                    UInt16 p = (UInt16)(j * Size + i);
                    values[p] = (Byte)(random.Next(0, Byte.MaxValue / density) == 0 ? 1 : 0);
                }
            }

            return values;
        }
    }
}
