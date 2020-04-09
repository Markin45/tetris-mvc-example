
using UnityEngine;

[CreateAssetMenu(menuName = "Figure Factory")]
public class FigureFactory : ScriptableObject, IFigureFactory
{
    public Figure[] figures;

    public IFigure RandomFigure()
    {
        return figures[Random.Range(0, figures.Length)];
    }
}
