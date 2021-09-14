namespace DrawingInConsole.Shape
{
	public class Rectangle : Shape
	{
		public uint X1 { get; }
		public uint Y1 { get; }
		public uint X2 { get; }
		public uint Y2 { get; }
		public char Colour { get; }

		protected Rectangle() { }

		public Rectangle(uint x1, uint y1, uint x2, uint y2)
		{
			Colour = ' ';
			X1 = x1;
			Y1 = y1;
			X2 = x2;
			Y2 = y2;
		}
	}
}
