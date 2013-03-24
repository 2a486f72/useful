namespace Useful
{
	using System;

	/// <summary>
	/// Argument validation helper methods.
	/// </summary>
	public static class Argument
	{
		public static void ValidateIsNotNull<T>(T value, string name)
		{
			if (value == null)
				throw new ArgumentNullException(name);
		}

		public static void ValidateIsNotNullOrWhitespace(string value, string name)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Value cannot be null or a string consisting of whitespace.", name);
		}

		public static void ValidateRange<T>(T value, string name, T? min = null, T? max = null)
			where T : struct, IComparable
		{
			if (min != null)
			{
				if (value.CompareTo(min) == -1)
					throw new ArgumentOutOfRangeException(name, string.Format("Cannot be less than {0}", min));
			}

			if (max != null)
			{
				if (value.CompareTo(max) == 1)
					throw new ArgumentOutOfRangeException(name, string.Format("Cannot be greater than {0}", max));
			}
		}

		public static void ValidateLength(Array array, string name, int? exact = null, int? min = null, int? max = null)
		{
			if (exact != null)
			{
				if (array.Length != exact)
					throw new ArgumentException(string.Format("Array must have {0} elements.", exact), name);
			}

			if (min != null)
			{
				if (array.Length < min)
					throw new ArgumentException(string.Format("Array must have at least {0} elements.", min), name);
			}

			if (max != null)
			{
				if (array.Length > max)
					throw new ArgumentException(string.Format("Array must have no more than {0} elements.", max), name);
			}
		}

		public static void ValidateEnum<T>(T value, string name)
			where T : struct
		{
			if (!Enum.IsDefined(value.GetType(), value))
				throw new ArgumentOutOfRangeException(name, "Undefined enumeration value.");
		}
	}
}