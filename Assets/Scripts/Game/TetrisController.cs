
public class TetrisController : ITetrisController
{
    public ITetrisModel Model { get; private set; }
    public IFigureFactory FigureFactory { get; private set; }

    public TetrisController(ITetrisModel model, IFigureFactory figureFactory)
    {
        Model = model;
        FigureFactory = figureFactory;
    }

    public void MoveFigureLeft()
    {
        TetrisUtil.MoveFigureLeft(Model);
    }

    public void MoveFigureRight()
    {
         TetrisUtil.MoveFigureRight(Model);
    }

    public bool Step()
    {
        if (Model.Figure == null) {
            TetrisUtil.SpawnFigure(Model, FigureFactory);
            if (TetrisUtil.FigureCollides(Model))
                return false;
        }

        if (!TetrisUtil.MoveFigureDown(Model)) {
            TetrisUtil.ImprintFigure(Model);
            TetrisUtil.EraseCompleteLines(Model);
            Model.Figure = null;
        }

        return true;
    }
}
