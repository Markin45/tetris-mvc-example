
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityTetrisController : MonoBehaviour
{
    public TetrisController Controller { get; private set; }
    public FigureFactory FigureFactory;

    void Start()
    {
        var model = FindObjectOfType<UnityTetrisModel>().Model;
        Controller = new TetrisController(model, FigureFactory);
        StartCoroutine(Run());
    }

    public void MoveFigureLeft()
    {
        Controller.MoveFigureLeft();
    }

    public void MoveFigureRight()
    {
        Controller.MoveFigureRight();
    }

    IEnumerator Run()
    {
        for (;;) {
            yield return new WaitForSeconds(0.1f);
            if (!Controller.Step())
                SceneManager.LoadScene("GameOver");
        }
    }
}
