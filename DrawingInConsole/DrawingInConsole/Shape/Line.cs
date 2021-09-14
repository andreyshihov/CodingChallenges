namespace DrawingInConsole.Shape
{
	public class Line : Shape
	{
		public uint X1 { get; }
		public uint Y1 { get; }
		public uint X2 { get; }
		public uint Y2 { get; }

		protected Line() { }

		public Line(uint x1, uint y1, uint x2, uint y2)
		{
			X1 = x1;
			Y1 = y1;
			X2 = x2;
			Y2 = y2;
		}
	}
}
