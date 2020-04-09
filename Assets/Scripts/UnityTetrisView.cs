using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnityTetrisView : MonoBehaviour
{
    ITetrisModel model;
    public Tilemap MainMap;
    public Tilemap FigureMap;
    public Tile EmptyTile;
    public Tile FullTile;
    public Tile FigureTile;

    void Start()
    {
        model = FindObjectOfType<UnityTetrisModel>().Model;
        model.Changed += Refresh;
        Refresh();
    }

    void OnDestroy()
    {
        model.Changed -= Refresh;
    }

    void Refresh()
    {
        UpdateField();
        UpdateFigure();
    }

    void UpdateField()
    {
        int w = model.Width;
        int h = model.Height;

        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++)
                MainMap.SetTile(new Vector3Int(x, h - y - 1, 0), (model.At(x, y) ? FullTile : EmptyTile));
        }
    }

    void UpdateFigure()
    {
        FigureMap.ClearAllTiles();

        if (model.Figure == null)
            return;

        int w = model.Figure.Width;
        int h = model.Figure.Height;

        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++) {
                if (model.Figure.At(x, y))
                    FigureMap.SetTile(new Vector3Int(model.FigureX + x, model.Height - (model.FigureY + y) - 1, 0), FigureTile);
            }
        }
    }
}
