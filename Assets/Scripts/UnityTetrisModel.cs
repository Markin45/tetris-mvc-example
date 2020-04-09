
using UnityEngine;

public class UnityTetrisModel : MonoBehaviour
{
    public int Width;
    public int Height;
    public ITetrisModel Model { get; private set; }

    void Awake()
    {
        Model = new TetrisModel(Width, Height);
    }
}
