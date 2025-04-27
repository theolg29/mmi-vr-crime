using UnityEngine;

public class BulletLineRenderer : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    [Header("Offset du Start Point")]
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    void Update()
    {
        if (startPoint != null && endPoint != null)
        {
            Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
            Vector3 startPos = startPoint.position + offset;

            line.SetPosition(0, startPos);
            line.SetPosition(1, endPoint.position);
        }
    }
}
