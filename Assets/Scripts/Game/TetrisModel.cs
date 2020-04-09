
public class TetrisModel : ITetrisModel
{
    IFigure figure;
    int figureX;
    int figureY;
    bool[] field;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public IFigure Figure { get { return figure; } set { figure = value; Changed?.Invoke(); } }
    public int FigureX { get { return figureX; } set { figureX = value; Changed?.Invoke(); } }
    public int FigureY { get { return figureY; } set { figureY = value; Changed?.Invoke(); } }

    public delegate void OnChanged();
    public event OnChanged Changed;

    public TetrisModel(int w, int h)
    {
        figure = null;
        figureX = 0;
        figureY = 0;
        Width = w;
        Height = h;
        field = new bool[w * h];
    }

    public bool At(int x, int y)
    {
        return field[y * Width + x];
    }

    public void SetAt(int x, int y, bool flag)
    {
        field[y * Width + x] = flag;
        Changed?.Invoke();
    }
}
