using DrawingInConsole.AreaTool;
using DrawingInConsole.Shape;
using DrawingInConsole.ShapeTool;

namespace DrawingInConsole.ToolFactory
{
	public class ConsoleMatrixToolFactory : MatrixToolFactory
	{
		public override IMatrixAreaFillingTool CreateFillingTool(uint startX, uint startY, char colour)
			=> new MatrixBfsAreaFillingTool(startX, startY, colour);

		public override IMatrixShapeTool CreateLineTool(uint x1, uint y1, uint x2, uint y2)
			=> new MatrixLineTool(new Line(x1, y1, x2, y2));

		public override IMatrixShapeTool CreateRectangleTool(uint x1, uint y1, uint x2, uint y2)
			=> new MatrixRectangleTool(new Rectangle(x1, y1, x2, y2));
	}
}
