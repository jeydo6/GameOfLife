using GameOfLife.Domain.Enumerations;
using System;

namespace GameOfLife.Application.Dto
{
	/// <summary>
	/// Add new field
	/// </summary>
	public class AddFieldDto
	{
		/// <summary>
		/// Size of the field, must be bigger than 2
		/// </summary>
		public UInt16 Size { get; set; }

		/// <summary>
		/// Density of points in the field
		/// </summary>
		public Byte Density { get; set; }

		/// <summary>
		/// Rules of the Life
		/// </summary>
		public BehaviorEnum BehaviorEnum { get; set; }
	}
}
