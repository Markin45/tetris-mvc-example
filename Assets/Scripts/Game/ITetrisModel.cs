
public interface ITetrisModel
{
    int Width { get; }
    int Height { get; }
    IFigure Figure { get; set; }
    int FigureX { get; set; }
    int FigureY { get; set; }

    event TetrisModel.OnChanged Changed;

    bool At(int x, int y);
    void SetAt(int x, int y, bool flag);
}
