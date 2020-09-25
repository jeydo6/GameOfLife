using System;

namespace GameOfLife.Application.Dto
{
    public class FieldDto
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Byte[] Values { get; set; }
    }
}
