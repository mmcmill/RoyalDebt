using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // a 2pt line renderer
    public LineRenderer lineRenderer;
    public float X_LEN_BAR;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 endOfbar = lineRenderer.GetPosition(1);
        endOfbar.x = X_LEN_BAR;
        lineRenderer.SetPosition(1, endOfbar);
    }

    public void UpdateBar(float currentNum, float maxNum)
    {
        Vector3 endOfBar = lineRenderer.GetPosition(1);
        endOfBar.x = (currentNum / maxNum) * X_LEN_BAR;
        lineRenderer.SetPosition(1, endOfBar);
    }
}
