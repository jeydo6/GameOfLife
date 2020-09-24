using System;

namespace GameOfLife.Domain.Entities
{
    public class Field
    {
        private readonly Action<Field> _lifeAction;

        public Field(UInt16 size, Action<Field> lifeAction)
        {
            Id = Guid.NewGuid();
            Values = new Boolean[size, size];

            _lifeAction = lifeAction;
        }

        public Guid Id { get; }

        public UInt32 Generation { get; private set; }

        public Boolean[,] Values { get; }

        public UInt16 Size
        {
            get
            {
                return (UInt16)Math.Sqrt(Values.Length);
            }
        }

        public Field Next()
        {
            _lifeAction?.Invoke(this);

            Generation++;

            return this;
        }
    }
}
