using DrawingInConsole.AreaTool;
using DrawingInConsole.ShapeTool;

namespace DrawingInConsole.ToolFactory
{
	public abstract class MatrixToolFactory
	{
		public abstract IMatrixAreaFillingTool CreateFillingTool(uint startX, uint startY, char colour);

		public abstract IMatrixShapeTool CreateLineTool(uint x1, uint y1, uint x2, uint y2);

		public abstract IMatrixShapeTool CreateRectangleTool(uint x1, uint y1, uint x2, uint y2);
	}
}
