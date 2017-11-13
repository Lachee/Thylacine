using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
	public struct Color
	{
		public byte R { get; set; }
		public byte G { get; set; }
		public byte B { get; set; }
		


		public int ToRGB() { return (R << 16) | (G << 8) | B; }
		public static Color FromRGB(int rgb)
		{
			return new Color()
			{ 
				R = (byte)((rgb >> 16) & byte.MaxValue),
				G = (byte)((rgb >> 8) & byte.MaxValue),
				B = (byte)(rgb & byte.MaxValue)
			};
		}

		public static implicit operator int(Color color)
		{
			return color.ToRGB();
		}
		public static explicit operator Color(int rgb)
		{
			return Color.FromRGB(rgb);
		}
	}
}
