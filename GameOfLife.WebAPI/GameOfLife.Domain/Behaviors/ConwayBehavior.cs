using System;
using System.Collections.Generic;

namespace GameOfLife.Domain.Behaviors
{
    public class ConwayBehavior : IBehavior
    {
        private static readonly IDictionary<Byte, Byte> _store = new Dictionary<Byte, Byte>();

        static ConwayBehavior()
        {
            for (Int32 i = 0; i <= Byte.MaxValue; i++)
            {
                Int32 number = i;
                Byte neighboursCount = 0;

                while (number > 0)
                {
                    neighboursCount++;
                    number &= number - 1;
                }

                _store.Add((Byte)i, neighboursCount);
            }
        }

        public Byte[] Generate(UInt16 size, Byte density, Int32 seed)
        {
            Byte[] result = new Byte[size * size];

            Random random = new Random(seed);
            for (UInt16 i = 1; i < size - 1; i++)
            {
                for (UInt16 j = 1; j < size - 1; j++)
                {
                    UInt32 p = (UInt32)(j * size + i);
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
                    UInt32 p = (UInt32)(j * size + i);

                    Byte neighboursId = GetNeighboursId(
                        values,
                        GetNeighboursPositions(size, x: i, y: j)
                    );

                    result[p] = GetNextValue(
                        values[p],
                        _store[neighboursId]
                    );
                }
            }

            return result;
        }

        private Byte GetNextValue(Byte value, Byte neighboursCount)
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
                    return 1;
                }
            }

            return 0;
        }

        private UInt16[] GetNeighboursPositions(UInt16 size, UInt16 x, UInt16 y)
        {
            return new UInt16[8]
            {
                (UInt16)((y - 1) * size + x - 1),
                (UInt16)((y - 1) * size + x),
                (UInt16)((y - 1) * size + x + 1),
                (UInt16)(y * size + x - 1),
                (UInt16)(y * size + x + 1),
                (UInt16)((y + 1) * size + x - 1),
                (UInt16)((y + 1) * size + x),
                (UInt16)((y + 1) * size + x + 1)
            };
        }

        private Byte GetNeighboursId(Byte[] values, UInt16[] positions)
        {
            return (Byte)(
                1 * values[positions[0]]
                + 2 * values[positions[1]]
                + 4 * values[positions[2]]
                + 8 * values[positions[3]]
                + 16 * values[positions[4]]
                + 32 * values[positions[5]]
                + 64 * values[positions[6]]
                + 128 * values[positions[7]]
            );
        }
    }
}
