using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    private bool trackMouse = false;
    [SerializeField] private GameObject drawController;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }

            if (_instance == null)
            {
                _instance = new GameObject("GameController", typeof(GameController)).GetComponent<GameController>();
            }

            return _instance;
        }
    }


    public void ChangeNewColor(Color color)
    {
        var alpha = 1f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );

        var linePrefab = (GameObject)Resources.Load("Prefabs/DrawLine");
        var lineRenderer = linePrefab.GetComponent<LineRenderer>();

        lineRenderer.colorGradient = gradient;
    }

}
