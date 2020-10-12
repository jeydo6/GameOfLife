using System;

namespace GameOfLife.Application.Dto
{
    public class FieldDto
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Values
        /// </summary>
        public Byte[] Data { get; set; }
    }
}
