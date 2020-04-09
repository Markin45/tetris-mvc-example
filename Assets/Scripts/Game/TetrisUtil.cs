
public static class TetrisUtil
{
    public static void SpawnFigure(ITetrisModel model, IFigureFactory figureFactory)
    {
        model.Figure = figureFactory.RandomFigure();
        model.FigureX = (model.Width - model.Figure.Width) / 2;
        model.FigureY = 0;
    }

    public static bool MoveFigureLeft(ITetrisModel model)
    {
        if (model.Figure == null)
            return false;

        if (model.FigureX > 0) {
            --model.FigureX;
            if (!FigureCollides(model))
                return true;
            ++model.FigureX;
        }

        return false;
    }

    public static bool MoveFigureRight(ITetrisModel model)
    {
        if (model.Figure == null)
            return false;

        if (model.FigureX + model.Figure.Width < model.Width) {
            ++model.FigureX;
            if (!FigureCollides(model))
                return true;
            --model.FigureX;
        }

        return false;
    }

    public static bool MoveFigureDown(ITetrisModel model)
    {
        if (model.Figure == null)
            return false;

        if (model.FigureY + model.Figure.Height < model.Height) {
            ++model.FigureY;
            if (!FigureCollides(model))
                return true;
            --model.FigureY;
        }

        return false;
    }

    public static bool FigureCollides(ITetrisModel model)
    {
        if (model.Figure == null)
            return false;

        int w = model.Figure.Width;
        int h = model.Figure.Height;

        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++) {
                if (model.Figure.At(x, y) && model.At(model.FigureX + x, model.FigureY + y))
                    return true;
            }
        }

        return false;
    }

    public static void ImprintFigure(ITetrisModel model)
    {
        if (model.Figure == null)
            return;

        int w = model.Figure.Width;
        int h = model.Figure.Height;

        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++) {
                if (model.Figure.At(x, y))
                    model.SetAt(model.FigureX + x, model.FigureY + y, true);
            }
        }
    }

    public static bool IsFullLine(ITetrisModel model, int y)
    {
        for (int x = 0; x < model.Width; x++) {
            if (!model.At(x, y))
                return false;
        }
        return true;
    }

    public static void ScrollDownToLine(ITetrisModel model, int endY)
    {
        int w = model.Width;

        for (int y = endY; y > 0; y--) {
            for (int x = 0; x < w; x++)
                model.SetAt(x, y, model.At(x, y - 1));
        }

        for (int x = 0; x < w; x++)
            model.SetAt(x, 0, false);
    }

    public static void EraseCompleteLines(ITetrisModel model)
    {
        int h = model.Height;
        for (int y = h - 1; y >= 0; y--) {
            if (IsFullLine(model, y)) {
                ScrollDownToLine(model, y);
                ++y;
            }
        }
    }
}
