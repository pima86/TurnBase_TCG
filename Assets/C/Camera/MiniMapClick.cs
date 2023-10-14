using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapClick : MonoBehaviour
{
    public static MiniMapClick Inst { get; private set; }

    [SerializeField] GameObject MainCam;
    [SerializeField] SpriteRenderer mapRenderer;

    Camera cam;
    Camera maincam;
    Vector3 Mouse;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        Inst = this;
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        maincam = MainCam.GetComponent<Camera>();

        float Ratio = (float)Screen.width / (float)Screen.height;
        if (Ratio > 1.65f)
        {
           // cam.rect = new Rect(0.0f, -8.5f, 1.0f, 1.0f);
        }
        else if (Ratio < 1.65f)
        {
           // cam.rect = new Rect(0.0f, -8.15f, 1.0f, 1.0f);
        }
    }

    public bool MapClick = false;
    public bool MapDown = false;
    void Update()
    {
        if (!StoryCam.Inst.CamMove_sub)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Mouse = Input.mousePosition;
                Mouse = cam.ScreenToWorldPoint(Mouse);
                if (Mouse.y < 44.5f)
                    MapDown = true;
            }
            if (Input.GetMouseButton(0) && MapDown)
            {
                Mouse = Input.mousePosition;
                Mouse = cam.ScreenToWorldPoint(Mouse);
                if (Mouse.x > -28 && Mouse.y < 44.5f)
                {
                    MapClick = true;
                    MainCam.transform.position = ClampCamera(Mouse + new Vector3(0, 0, 700));
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                MapDown = false;
                MapClick = false;
            }
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = maincam.orthographicSize;
        float camWidth = maincam.orthographicSize * maincam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
