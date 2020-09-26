using System;

namespace GameOfLife.Domain.Behaviors
{
    public class ConwayBehavior : IBehavior
    {
        public Byte[] Generate(UInt16 size, Byte density, Int32 seed)
        {
            Byte[] result = new Byte[size * size];

            Random random = new Random(seed);
            for (UInt16 i = 1; i < size - 1; i++)
            {
                for (UInt16 j = 1; j < size - 1; j++)
                {
                    UInt16 p = (UInt16)(j * size + i);
                    result[p] = (Byte)(random.Next(0, Byte.MaxValue / density) == 0 ? 1 : 0);
                }
            }

            return result;
        }

        public Byte[] Iterate(Byte[] values)
        {
            UInt16 size = (UInt16)Math.Sqrt(values.Length);
            Byte[] result = new Byte[size * size];
            values.CopyTo(result, 0);

            for (UInt16 i = 1; i < size - 1; i++)
            {
                for (UInt16 j = 1; j < size - 1; j++)
                {
                    UInt16 p = (UInt16)(j * size + i);

                    result[p] = GetValue(
                        values[p],
                        GetNeighboursCount(values, size, x: i, y: j)
                    );
                }
            }

            return result;
        }

        private Byte GetNeighboursCount(Byte[] values, UInt16 size, UInt16 x, UInt16 y)
        {
            Byte count1 = 0;

            for (UInt16 i = (UInt16)(x - 1); i <= (UInt16)(x + 1); i++)
            {
                for (UInt16 j = (UInt16)(y - 1); j <= (UInt16)(y + 1); j++)
                {
                    UInt16 p = (UInt16)(j * size + i);

                    if (i == x && j == y)
                    {
                        continue;
                    }

                    if (values[p] == 1)
                    {
                        count1++;
                    }
                }
            }

            return count1;
        }

        private Byte GetValue(Byte value, Byte neighboursCount)
        {
            if (value == 1)
            {
                if (neighboursCount == 2 || neighboursCount == 3)
                {
                    return 1;
                }
            }
            else if (value == 0)
            {
                if (neighboursCount == 3)
                {
                    return 0;
                }
            }

            return 0;
        }
    }
}
