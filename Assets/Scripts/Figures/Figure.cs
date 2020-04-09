
using UnityEngine;

[CreateAssetMenu(menuName = "Figure")]
public class Figure : ScriptableObject, IFigure
{
    public const int MaxWidth = 4;
    public const int MaxHeight = 4;

    [SerializeField] private bool[] blocks = new bool[MaxWidth * MaxHeight];

    public int Width { get {
            int w = 0;
            for (int y = 0; y < MaxHeight; y++) {
                int lineW = 0;
                for (int x = 0; x < MaxWidth; x++) {
                    if (At(x, y))
                        lineW = x + 1;
                }

                if (lineW > w)
                    w = lineW;
            }
            return w;
        } }

    public int Height { get {
            int h = 0;
            for (int y = 0; y < MaxHeight; y++) {
                bool emptyLine = true;
                for (int x = 0; x < MaxWidth; x++) {
                    if (At(x, y)) {
                        emptyLine = false;
                        break;
                    }
                }
                if (!emptyLine)
                    h = y + 1;
            }
            return h;
        } }

    public bool At(int x, int y)
    {
        return blocks[y * MaxWidth + x];
    }

    #if UNITY_EDITOR
    public void EditorSetAt(int x, int y, bool value)
    {
        blocks[y * MaxWidth + x] = value;
    }
    #endif
}
